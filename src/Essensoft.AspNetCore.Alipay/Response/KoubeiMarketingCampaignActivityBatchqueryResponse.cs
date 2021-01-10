using Newtonsoft.Json;
using System.Collections.Generic;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignActivityBatchqueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignActivityBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        [JsonProperty("camp_sets")]
        public List<CampBaseDto> CampSets { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        [JsonProperty("total_number")]
        public string TotalNumber { get; set; }
    }
}
