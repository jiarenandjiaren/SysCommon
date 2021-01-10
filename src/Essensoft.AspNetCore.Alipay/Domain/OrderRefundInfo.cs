using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// OrderRefundInfo Data Structure.
    /// </summary>
    public class OrderRefundInfo : AlipayObject
    {
        /// <summary>
        /// 服务订单的退款金额，单位为元
        /// </summary>
        [JsonProperty("refund_amount")]
        public string RefundAmount { get; set; }

        /// <summary>
        /// 服务订单号，当商家退款时，可能包含多个服务订单
        /// </summary>
        [JsonProperty("service_order_no")]
        public string ServiceOrderNo { get; set; }
    }
}
