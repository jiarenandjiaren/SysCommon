using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// ZhimaMerchantOrderRentCompleteResponse.
    /// </summary>
    public class ZhimaMerchantOrderRentCompleteResponse : AlipayResponse
    {
        /// <summary>
        /// 资金流水号，用于商户与支付宝进行对账
        /// </summary>
        [JsonProperty("alipay_fund_order_no")]
        public string AlipayFundOrderNo { get; set; }

        /// <summary>
        /// 信用借还的订单号
        /// </summary>
        [JsonProperty("order_no")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 借用人支付宝userId.
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
