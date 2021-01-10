﻿using System.Xml.Serialization;

namespace Essensoft.AspNetCore.WeChatPay.Response
{
    public class WeChatPayGetTransferInfoResponse : WeChatPayResponse
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        [XmlElement("partner_trade_no")]
        public string PartnerTradeNo { get; set; }

        /// <summary>
        /// 付款单号
        /// </summary>
        [XmlElement("detail_id")]
        public string DetailId { get; set; }

        /// <summary>
        /// 转账状态
        /// </summary>
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        [XmlElement("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        [XmlElement("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 收款用户姓名
        /// </summary>
        [XmlElement("transfer_name")]
        public string TransferName { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        [XmlElement("payment_amount")]
        public int PaymentAmount { get; set; }

        /// <summary>
        /// 转账时间
        /// </summary>
        [XmlElement("transfer_time")]
        public string TransferTime { get; set; }

        /// <summary>
        /// 付款描述
        /// </summary>
        [XmlElement("desc")]
        public string Desc { get; set; }
    }
}
