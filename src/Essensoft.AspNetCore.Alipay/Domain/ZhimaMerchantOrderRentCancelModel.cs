using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// ZhimaMerchantOrderRentCancelModel Data Structure.
    /// </summary>
    public class ZhimaMerchantOrderRentCancelModel : AlipayObject
    {
        /// <summary>
        /// 信用借还订单号
        /// </summary>
        [JsonProperty("order_no")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 信用借还的产品码:w1010100000000002858
        /// </summary>
        [JsonProperty("product_code")]
        public string ProductCode { get; set; }
    }
}
