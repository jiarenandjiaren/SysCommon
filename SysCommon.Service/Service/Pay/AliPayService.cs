using Essensoft.AspNetCore.Alipay;
using Essensoft.AspNetCore.Alipay.Domain;
using Essensoft.AspNetCore.Alipay.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Essensoft.AspNetCore.Alipay.Notify;
using SysCommon.Service.Request;
using System;
using Infrastructure.PayConfig;
using Alipay.AopSdk.F2FPay.Domain;
using Alipay.AopSdk.F2FPay.Business;
using System.Drawing;
using System.IO;
using Alipay.AopSdk.F2FPay.Model;
using System.Threading;
using System.Drawing.Imaging;
using System.Collections.Generic;
using QRCoder;
using Newtonsoft.Json.Linq;
using SysCommon.Repository.Domain;
using Microsoft.AspNetCore.Hosting;
using Alipay.AopSdk.F2FPay.AspnetCore;
using GoodsInfo = Alipay.AopSdk.F2FPay.Model.GoodsInfo;
using System.Text;

namespace SysCommon.Service
{
    public class AlipayService : Controller
    {
        public readonly AlipayClient _client = null;
        private readonly AlipayNotifyClient _notifyClient = null;
        private readonly AllPayService _allPayService;
        private readonly IAlipayF2FService _alipayF2FService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AlipayService(AlipayClient client, AlipayNotifyClient notifyClient, AllPayService allPayService, IAlipayF2FService alipayF2FService, IHostingEnvironment hostingEnvironment  )
        {
            _allPayService = allPayService;
            _client = client;
            _notifyClient = notifyClient;
            _alipayF2FService = alipayF2FService;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 电脑网站支付
        /// <summary>
        /// 电脑网站支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="subject"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <param name="product_code"></param>
        /// <param name="notify_url"></param>
        /// <param name="return_url"></param>
        /// <returns></returns>
        public async Task<string> PagePay(string subject, string total_amount, string body, string product_code, string notify_url, string return_url)
        {
            string out_trade_no = Common.GenerateOutTradeNo();
            var model = new AlipayTradePagePayModel()
            {
                Body = body,
                Subject = subject,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no,
                ProductCode = product_code,
            };
            var req = new AlipayTradePagePayRequest();
            req.SetBizModel(model);
            req.SetNotifyUrl(notify_url);
            req.SetReturnUrl(return_url);

            var response = await _client.PageExecuteAsync(req, null, "GET");
            return response.Body;
        }
        #endregion
        #region 手机网站支付
        /// <summary>
        /// 手机网站支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="subject"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <param name="product_code"></param>
        /// <param name="notify_url"></param>
        /// <param name="return_url"></param>
        /// <returns></returns>
        public async Task<string> WapPay(string subject, string total_amount, string body, string product_code, string notify_url, string return_url)
        {
            string out_trade_no = Common.GenerateOutTradeNo();
            var model = new AlipayTradeWapPayModel()
            {
                Body = body,
                Subject = subject,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no,
                ProductCode = product_code,
            };
            var req = new AlipayTradeWapPayRequest();
            req.SetBizModel(model);
            req.SetNotifyUrl(notify_url);
            req.SetReturnUrl(return_url);

            var response = await _client.PageExecuteAsync(req, null, "GET");
            //Redirect(response.Body);
            return response.Body;
        }
        #endregion
        #region 扫码支付 生成支付二维码
        /// <summary>
        /// 扫码支付 生成支付二维码
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单号</param>
        /// <returns></returns>
        public IActionResult PreCreate(AAllPayReq aAllPayReq)
        {
            string outTradeNo = Common.GenerateOutTradeNo();
            AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent(aAllPayReq.orderName, aAllPayReq.orderAmount, outTradeNo);

            //如果需要接收扫码支付异步通知，那么请把下面两行注释代替本行。
            //推荐使用轮询撤销机制，不推荐使用异步通知,避免单边账问题发生。
            AlipayF2FPrecreateResult precreateResult = _alipayF2FService.TradePrecreate(builder);
            //string notify_url = "http://10.5.21.14/Pay/Notify";  //商户接收异步通知的地址
            //AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder, notify_url);

            //以下返回结果的处理供参考。
            //payResponse.QrCode即二维码对于的链接
            //将链接用二维码工具生成二维码打印出来，顾客可以用支付宝钱包扫码支付。
            var bitmap = new Bitmap(Path.Combine(_hostingEnvironment.WebRootPath, "images/error.png"));
            switch (precreateResult.Status)
            {
                case ResultEnum.SUCCESS:
                    bitmap.Dispose();
                    bitmap = RenderQrCode(precreateResult.response.QrCode);
                    ////轮询订单结果
                    ////根据业务需要，选择是否新起线程进行轮询
                    ParameterizedThreadStart parStart = new ParameterizedThreadStart(LoopQuery);
                    Thread myThread = new Thread(parStart);
                    object o = precreateResult.response.OutTradeNo;
                    myThread.Start(o);
                    break;
                case ResultEnum.FAILED:
                    Console.WriteLine("生成二维码失败：" + precreateResult.response.Body);
                    break;

                case ResultEnum.UNKNOWN:
                    Console.WriteLine("生成二维码失败：" + (precreateResult.response == null ? "配置或网络异常，请检查后重试" : "系统异常，请更新外部订单后重新发起请求"));
                    break;
            }
            //入库基本支付信息
            Payment requser = aAllPayReq;
            requser.OutTradeNo = outTradeNo;
            requser.CreateTime = DateTime.Now;
            //_allPayService.Add(requser);

            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            byte[] bytes = ms.GetBuffer();
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(bitmap.ToString(), QRCodeGenerator.ECCLevel.L))
            using (var svgQrCode = new SvgQRCode(qrCodeData))
            {
                var svgText = svgQrCode.GetGraphic(new Size(140, 140));
                var cc = File(Encoding.UTF8.GetBytes(svgText), "text/xml");
                return cc;
            }
               
            
            
            //return File(Encoding.UTF8.GetBytes(svgText), "text/xml");

          
        }
        /// <summary>
        /// 构造支付请求数据
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单编号</param>
        /// <returns>请求结果集</returns>
        private AlipayTradePrecreateContentBuilder BuildPrecreateContent(string orderName, string orderAmount, string outTradeNo)
        {
            //线上联调时，请输入真实的外部订单号。
            if (string.IsNullOrEmpty(outTradeNo))
            {
                outTradeNo = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
            }

            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
            //收款账号


            builder.seller_id = AliConfig.Uid;



            //订单编号
            builder.out_trade_no = outTradeNo;
            //订单总金额
            builder.total_amount = orderAmount;
            //参与优惠计算的金额
            //builder.discountable_amount = "";
            //不参与优惠计算的金额
            //builder.undiscountable_amount = "";
            //订单名称
            builder.subject = orderName;
            //自定义超时时间
            builder.timeout_express = "5m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "test store id";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = "test";

            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();
            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = "goods id";
            goods.goods_name = "goods name";
            goods.price = "0.01";
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;

            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;

            return builder;

        }
        /// <summary>
        /// 渲染二维码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private Bitmap RenderQrCode(string str)
        {
            QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.L;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {

                        Bitmap bp = qrCode.GetGraphic(20, Color.Black, Color.White,
                            new Bitmap(Path.Combine(_hostingEnvironment.WebRootPath, "images/alipay.png")), 15);
                        return bp;
                    }
                }
            }

        }

        /// <summary>
        /// 轮询支付结果
        /// </summary>
        /// <param name="o">订单号</param>
        public void LoopQuery(object o)
        {
            AlipayF2FQueryResult queryResult = new AlipayF2FQueryResult();
            int count = 100;
            int interval = 10000;
            string outTradeNo = o.ToString();

            for (int i = 1; i <= count; i++)
            {
                Thread.Sleep(interval);
                queryResult = _alipayF2FService.TradeQuery(outTradeNo);
                if (queryResult?.Status == ResultEnum.SUCCESS)
                {
                    DoSuccessProcess(queryResult);
                    return;
                }
            }
            DoFailedProcess(queryResult);
        }

        /// <summary>
        /// 请添加支付成功后的处理
        /// </summary>
        private void DoSuccessProcess(AlipayF2FQueryResult queryResult)
        {
            string str = queryResult.response.Body;
            var jObject = JObject.Parse(str);
            //支付成功，请更新相应单据
            Payment requser = new Payment(); ;
            requser.UpdateTime = DateTime.Now;
            requser.OutTradeNo = Convert.ToString(jObject["alipay_trade_query_response"]["out_trade_no"].ToString());
            requser.PayType = "Ali";
            requser.BuyerLogonId = Convert.ToString(jObject["alipay_trade_query_response"]["buyer_logon_id"].ToString());
            requser.TradeNo = Convert.ToString(jObject["alipay_trade_query_response"]["trade_no"].ToString());
            requser.TotalAmount = Convert.ToDouble(jObject["alipay_trade_query_response"]["total_amount"].ToString());
            requser.SendPayDate = Convert.ToString(jObject["alipay_trade_query_response"]["send_pay_date"].ToString());
            requser.ReceiptAmount = Convert.ToString(jObject["alipay_trade_query_response"]["receipt_amount"].ToString());
            requser.PointAmount = Convert.ToString(jObject["alipay_trade_query_response"]["point_amount"].ToString());
            requser.TradeStatus = Convert.ToString(jObject["alipay_trade_query_response"]["trade_status"].ToString());
            requser.InvoiceAmount = Convert.ToDouble(jObject["alipay_trade_query_response"]["invoice_amount"].ToString());
            //requser.FundBillList = Convert.ToString(jObject["alipay_trade_query_response"]["fund_bill_list"].ToString());
            requser.BuyerUserId = Convert.ToString(jObject["alipay_trade_query_response"]["buyer_user_id"].ToString());
            requser.BuyerPayAmount = Convert.ToDouble(jObject["alipay_trade_query_response"]["buyer_pay_amount"].ToString());
            requser.Msg = Convert.ToString(jObject["alipay_trade_query_response"]["msg"].ToString());
            requser.ByUserType = Convert.ToString(jObject["alipay_trade_query_response"]["buyer_user_type"].ToString());
            requser.PayState = true;

            _allPayService.Edit(requser);
            Console.WriteLine("扫码支付成功：商户订单号 " + queryResult.response.OutTradeNo);

        }
        /// <summary>
        /// 请添加支付失败后的处理
        /// </summary>
        private void DoFailedProcess(AlipayF2FQueryResult queryResult)
        {
            string str = queryResult.response.Body;
            var jObject = JObject.Parse(str);
            //支付失败，请更新相应单据

            Payment payment = new Payment();
            payment.UpdateTime = DateTime.Now;
            payment.OutTradeNo = Convert.ToString(jObject["alipay_trade_query_response"]["out_trade_no"].ToString());
            payment.PayType = "Ali";
            //payment.BuyerLogonId = Convert.ToString(jObject["alipay_trade_query_response"]["buyer_logon_id"].ToString());
            //payment.TradeNo = Convert.ToString(jObject["alipay_trade_query_response"]["trade_no"].ToString());
            //payment.TotalAmount = Convert.ToDouble(jObject["alipay_trade_query_response"]["total_amount"].ToString());
            //payment.SendPayDate = Convert.ToString(jObject["alipay_trade_query_response"]["send_pay_date"].ToString());
            payment.ReceiptAmount = Convert.ToString(jObject["alipay_trade_query_response"]["receipt_amount"].ToString());
            payment.PointAmount = Convert.ToString(jObject["alipay_trade_query_response"]["point_amount"].ToString());
            //payment.TradeStatus = Convert.ToString(jObject["alipay_trade_query_response"]["trade_status"].ToString());
            payment.InvoiceAmount = Convert.ToDouble(jObject["alipay_trade_query_response"]["invoice_amount"].ToString());
            //payment.FundBillList = Convert.ToString(jObject["alipay_trade_query_response"]["fund_bill_list"].ToString());
            //payment.BuyerUserId = Convert.ToString(jObject["alipay_trade_query_response"]["buyer_user_id"].ToString());
            payment.BuyerPayAmount = Convert.ToDouble(jObject["alipay_trade_query_response"]["buyer_pay_amount"].ToString());
            payment.Msg = Convert.ToString(jObject["alipay_trade_query_response"]["msg"].ToString());
            //payment.ByUserType = Convert.ToString(jObject["alipay_trade_query_response"]["buyer_user_type"].ToString());

            _allPayService.Edit(payment);

            Console.WriteLine("扫码支付失败：商户订单号 " + queryResult.response.OutTradeNo);
        }
        #endregion
        #region 条码支付
        /// <summary>
        /// 条码支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="scene"></param>
        /// <param name="auth_code"></param>
        /// <param name="subject"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <param name="notify_url"></param>
        /// <returns></returns>
        public async Task<IActionResult> Pay(string scene, string auth_code, string subject, string total_amount, string body, string notify_url)
        {
            string out_trade_no = Common.GenerateOutTradeNo();
            var builder = new AlipayTradePayModel()
            {
                Scene = scene,
                AuthCode = auth_code,
                Body = body,
                Subject = subject,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no,
            };
            var req = new AlipayTradePayRequest();
            req.SetBizModel(builder);
            req.SetNotifyUrl(notify_url);

            var response = await _client.ExecuteAsync(req);
            return Ok(response.Body);
        }
        #endregion
        #region 交易查询
        /// <summary>
        /// 交易查询
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <returns></returns>
        public async Task<string> Query(string out_trade_no, string trade_no)
        {
            var builder = new AlipayTradeQueryModel()
            {
                OutTradeNo = out_trade_no,
                TradeNo = trade_no
            };

            var req = new AlipayTradeQueryRequest();
            req.SetBizModel(builder);

            var response = await _client.ExecuteAsync(req);
            return response.Body;
        }
        #endregion
        #region 交易退款
        /// <summary>
        /// 交易退款
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <param name="refund_amount"></param>
        /// <param name="refund_reason"></param>
        /// <param name="out_request_no"></param>
        /// <returns></returns>
        public async Task<string> Refund(string out_trade_no, string trade_no, string refund_amount, string refund_reason)
        {
            string out_request_no = Common.GenerateOutTradeNo();
            var builder = new AlipayTradeRefundModel()
            {
                OutTradeNo = out_trade_no,
                TradeNo = trade_no,
                RefundAmount = refund_amount,
                OutRequestNo = out_request_no,
                RefundReason = refund_reason
            };

            var req = new AlipayTradeRefundRequest();
            req.SetBizModel(builder);

            var response = await _client.ExecuteAsync(req);
            return response.Body;
        }
        #endregion
        #region 退款查询
        /// <summary>
        /// 退款查询
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <param name="out_request_no"></param>
        /// <returns></returns>
        public async Task<string> RefundQuery(string out_trade_no, string trade_no, string out_request_no)
        {
            var builder = new AlipayTradeFastpayRefundQueryModel()
            {
                OutTradeNo = out_trade_no,
                TradeNo = trade_no,
                OutRequestNo = out_request_no
            };

            var req = new AlipayTradeFastpayRefundQueryRequest();
            req.SetBizModel(builder);

            var response = await _client.ExecuteAsync(req);
            return response.Body;
        }
        #endregion
        #region 关闭订单
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <returns></returns>
        public async Task<string> Close(string out_trade_no, string trade_no)
        {
            var builder = new AlipayTradeCloseModel()
            {
                OutTradeNo = out_trade_no,
                TradeNo = trade_no,
            };

            var req = new AlipayTradeCloseRequest();
            req.SetBizModel(builder);

            var response = await _client.ExecuteAsync(req);
            return response.Body;
        }
        #endregion
        #region 单笔转账
        /// <summary>
        /// 单笔转账
        /// </summary>
        /// <param name="out_biz_no"></param>
        /// <param name="payee_account"></param>
        /// <param name="payee_type"></param>
        /// <param name="amount"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Trans(string out_biz_no, string payee_account, string payee_type, string amount, string remark)
        {
            var builder = new AlipayFundTransToaccountTransferModel()
            {
                OutBizNo = out_biz_no,
                PayeeType = payee_type,
                PayeeAccount = payee_account,
                Amount = amount,
                Remark = remark
            };
            var req = new AlipayFundTransToaccountTransferRequest();
            req.SetBizModel(builder);
            var response = await _client.ExecuteAsync(req);
            return Ok(response.Body);
        }
        #endregion
        #region 转账查询
        /// <summary>
        /// 转账查询
        /// </summary>
        /// <param name="out_biz_no"></param>
        /// <param name="order_id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> TransQuery(string out_biz_no, string order_id)
        {
            var builder = new AlipayFundTransOrderQueryModel()
            {
                OutBizNo = out_biz_no,
                OrderId = order_id,
            };

            var req = new AlipayFundTransOrderQueryRequest();
            req.SetBizModel(builder);
            var response = await _client.ExecuteAsync(req);
            return Ok(response.Body);
        }
        #endregion 
        public async Task<IActionResult> BillDownloadurlQuery(string bill_date, string bill_type)
        {
            var builder = new AlipayDataDataserviceBillDownloadurlQueryModel()
            {
                BillDate = bill_date,
                BillType = bill_type
            };

            var req = new AlipayDataDataserviceBillDownloadurlQueryRequest();
            req.SetBizModel(builder);
            var response = await _client.ExecuteAsync(req);
            return Ok(response.Body);
        }

        [HttpGet]
        public IActionResult PagePayReturn()
        {
            try
            {
                var notify = _notifyClient.Execute<AlipayTradePagePayReturnResponse>(Request);
                return Content("success", "text/plain");
            }
            catch
            {
                return Content("error", "text/plain");
            }
        }

        [HttpGet]
        public IActionResult WapPayReturn()
        {
            try
            {
                var notify = _notifyClient.Execute<AlipayTradeWapPayReturnResponse>(Request);
                return Content("success", "text/plain");
            }
            catch
            {
                return Content("error", "text/plain");
            }
        }
    }
}