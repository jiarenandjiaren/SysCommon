using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// DataTag Data Structure.
    /// </summary>
    public class DataTag : AlipayObject
    {
        /// <summary>
        /// 聚合方式NONE,COUNT,COUNT_DISTINCT,DISTINCT,MIN,MAX,SUM
        /// </summary>
        [JsonProperty("aggregate")]
        public string Aggregate { get; set; }

        /// <summary>
        /// 列别名
        /// </summary>
        [JsonProperty("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// 标签CODE
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
