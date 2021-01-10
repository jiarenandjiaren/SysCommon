using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// LabelContext Data Structure.
    /// </summary>
    public class LabelContext : AlipayObject
    {
        /// <summary>
        /// 标签组发圈人的单个过滤器信息
        /// </summary>
        [JsonProperty("a")]
        public LabelFilter A { get; set; }
    }
}
