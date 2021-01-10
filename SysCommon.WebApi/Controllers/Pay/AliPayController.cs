using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysCommon.Service;
using SysCommon.Service.Request;

namespace SysCommon.WebApi.Controllers.Pay
{
    /// <summary>
    /// 支付-支付宝
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlipayController : ControllerBase
    {
        private readonly AlipayService _alipayService;

        public AlipayController(AlipayService alipayService)
        {
            _alipayService = alipayService;
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
        [HttpPost]
        public async Task<WebResponseContent<string>> PagePayAsync(string subject, string total_amount, string body, string product_code, string notify_url, string return_url)
        {
            var result = new WebResponseContent<string>();
            try
            {
                result.Data = await _alipayService.PagePay(subject, total_amount, body, product_code, notify_url, return_url);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
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
        [HttpPost]
        public async Task<WebResponseContent<string>> WapPay(string subject, string total_amount, string body, string product_code, string notify_url, string return_url)
        {
            var result = new WebResponseContent<string>();
            try
            {
                result.Data = await _alipayService.WapPay(subject, total_amount, body, product_code, notify_url, return_url);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 扫码支付
        /// <summary>
        /// 扫码支付
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单号</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ScanCodeGen([FromQuery] AAllPayReq aAllPayReq)
        {
            return _alipayService.PreCreate(aAllPayReq);
        }
        #endregion 
        #region 条码支付
        /// <summary>
        /// 条码支付（开发中）
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="scene"></param>
        /// <param name="auth_code"></param>
        /// <param name="subject"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <param name="notify_url"></param>
        /// <returns></returns>
        [HttpPost]
        public void Pay()
        {

        }
        #endregion 
        #region 交易查询
        /// <summary>
        /// 交易查询
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebResponseContent<string>> Query(string out_trade_no, string trade_no)
        {
            var result = new WebResponseContent<string>();
            try
            {
                result.Data = await _alipayService.Query(out_trade_no, trade_no);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 交易退款
        /// <summary>
        /// 交易退款
        /// </summary>
        /// <param name="out_trade_no">订单号</param>
        /// <param name="trade_no">支付宝流水号</param>
        /// <param name="refund_amount">退款金额</param>
        /// <param name="refund_reason">退款原因</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebResponseContent<string>> Refund(string out_trade_no, string trade_no, string refund_amount, string refund_reason)
        {
            var result = new WebResponseContent<string>();
            try
            {
                result.Data = await _alipayService.Refund(out_trade_no, trade_no, refund_amount, refund_reason);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion 
        #region 退款查询
        /// <summary>
        /// 退款查询
        /// </summary>
        /// <param name="out_trade_no">订单号</param>
        /// <param name="trade_no">流水号</param>
        /// <param name="out_request_no">退款单号</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebResponseContent<string>> RefundQuery(string out_trade_no, string trade_no, string out_request_no)
        {
            var result = new WebResponseContent<string>();
            try
            {
                result.Data = await _alipayService.RefundQuery(out_trade_no, trade_no, out_request_no);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 关闭订单
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<WebResponseContent<string>> Close(string out_trade_no, string trade_no)
        {
            var result = new WebResponseContent<string>();
            try
            {
                result.Data = await _alipayService.Close(out_trade_no, trade_no);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 单笔转账
        /// <summary>
        /// 单笔转账 (开发中)
        /// </summary>
        /// <param name="out_biz_no"></param>
        /// <param name="payee_account"></param>
        /// <param name="payee_type"></param>
        /// <param name="amount"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [HttpPost]
        public void Trans(string out_biz_no, string payee_account, string payee_type, string amount, string remark)
        {

        }
        #endregion
        #region 转账查询
        /// <summary>
        /// 转账查询 (开发中)
        /// </summary>
        /// <param name="out_biz_no"></param>
        /// <param name="order_id"></param>
        /// <returns></returns>
        [HttpPost]
        public void TransQuery(string out_biz_no, string order_id)
        {

        }
        #endregion
    }
}
