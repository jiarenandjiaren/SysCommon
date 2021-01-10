using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCampaignDiscountQueryModel Data Structure.
    /// </summary>
    public class AlipayMarketingCampaignDiscountQueryModel : AlipayObject
    {
        /// <summary>
        /// 活动id
        /// </summary>
        [JsonProperty("camp_id")]
        public string CampId { get; set; }
    }
}
