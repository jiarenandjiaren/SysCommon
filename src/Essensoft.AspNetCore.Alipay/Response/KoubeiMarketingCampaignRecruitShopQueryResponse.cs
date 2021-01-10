using Newtonsoft.Json;
using System.Collections.Generic;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignRecruitShopQueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignRecruitShopQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 招商活动id
        /// </summary>
        [JsonProperty("plan_id")]
        public string PlanId { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        [JsonProperty("shop_count")]
        public string ShopCount { get; set; }

        /// <summary>
        /// 招商门店确认详情
        /// </summary>
        [JsonProperty("shop_info")]
        public List<RecruitShopInfo> ShopInfo { get; set; }
    }
}
