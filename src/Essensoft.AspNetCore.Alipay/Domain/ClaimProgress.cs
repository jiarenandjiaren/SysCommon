using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// ClaimProgress Data Structure.
    /// </summary>
    public class ClaimProgress : AlipayObject
    {
        /// <summary>
        /// 更新内容
        /// </summary>
        [JsonProperty("update_content")]
        public string UpdateContent { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty("update_time")]
        public string UpdateTime { get; set; }
    }
}
