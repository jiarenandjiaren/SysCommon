using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCdpAdvertiseQueryModel Data Structure.
    /// </summary>
    public class AlipayMarketingCdpAdvertiseQueryModel : AlipayObject
    {
        /// <summary>
        /// 广告Id，唯一标识一条广告
        /// </summary>
        [JsonProperty("ad_id")]
        public string AdId { get; set; }
    }
}
