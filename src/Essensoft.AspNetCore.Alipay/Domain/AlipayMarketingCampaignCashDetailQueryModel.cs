using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCampaignCashDetailQueryModel Data Structure.
    /// </summary>
    public class AlipayMarketingCampaignCashDetailQueryModel : AlipayObject
    {
        /// <summary>
        /// 要查询的现金红包活动号
        /// </summary>
        [JsonProperty("crowd_no")]
        public string CrowdNo { get; set; }
    }
}
