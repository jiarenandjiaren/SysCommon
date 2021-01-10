/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： EAllPayReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:45   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Infrastructure;
using SysCommon.Repository.Core;
using SysCommon.Repository.Domain;

namespace SysCommon.Service.Request
{
    public class EAllPayReq
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string outTradeNo { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string UpdateId { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string orderName { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string orderAmount { get; set; }
        /// <summary>
        /// 支付类型（支付宝、微信）
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 买家账号
        /// </summary>
        public string BuyerLogonId { get; set; }
        /// <summary>
        /// 该交易在支付宝系统中的交易流水号，最长64位。
        /// </summary>
        public string TradeNo { get; set; }
        /// <summary>
        /// 订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]
        /// </summary>
        public double TotalAmount { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// /未定义
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 商户门店编号
        /// </summary>
        public string StoreId { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string SendPayDate { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string ReceiptAmount { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string PointAmount { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string TradeStatus { get; set; }
        /// <summary>
        /// 商户网站唯一订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public double InvoiceAmount { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string IndustrySepcDetail { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string FundBillList { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string DiscountGoodsDetail { get; set; }
        /// <summary>
        /// 商户Id
        /// </summary>
        public string BuyerUserId { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public double BuyerPayAmount { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string AlipayStoreId { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string VoucherDetailList { get; set; }
        /// <summary>
        /// 支付结果
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 未定义
        /// </summary>
        public string ByUserType { get; set; }

        public static implicit operator EAllPayReq(Payment payment)
        {
            return payment.MapTo<EAllPayReq>();
        }
        public static implicit operator Payment(EAllPayReq eAllPayReq)
        {
            return eAllPayReq.MapTo<Payment>();
        }
        public EAllPayReq()
        { }
    }
}