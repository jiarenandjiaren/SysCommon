using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignDetailInfoQueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignDetailInfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 适用门店:门店与门店之间用“,”隔开
        /// </summary>
        [JsonProperty("limit_shops")]
        public string LimitShops { get; set; }
    }
}
