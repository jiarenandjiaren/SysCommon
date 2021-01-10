using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayAssetPointOrderCreateResponse.
    /// </summary>
    public class AlipayAssetPointOrderCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝集分宝发放流水号
        /// </summary>
        [JsonProperty("alipay_order_no")]
        public string AlipayOrderNo { get; set; }
    }
}
