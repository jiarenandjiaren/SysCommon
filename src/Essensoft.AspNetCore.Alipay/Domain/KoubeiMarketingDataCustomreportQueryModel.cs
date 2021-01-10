using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// KoubeiMarketingDataCustomreportQueryModel Data Structure.
    /// </summary>
    public class KoubeiMarketingDataCustomreportQueryModel : AlipayObject
    {
        /// <summary>
        /// 规则KEY
        /// </summary>
        [JsonProperty("condition_key")]
        public string ConditionKey { get; set; }

        /// <summary>
        /// 额外增加的查询过滤条件
        /// </summary>
        [JsonProperty("filter_tags")]
        public List<FilterTag> FilterTags { get; set; }

        /// <summary>
        /// 一次拉多少条
        /// </summary>
        [JsonProperty("max_count")]
        public string MaxCount { get; set; }
    }
}
