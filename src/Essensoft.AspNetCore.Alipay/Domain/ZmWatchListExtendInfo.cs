using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// ZmWatchListExtendInfo Data Structure.
    /// </summary>
    public class ZmWatchListExtendInfo : AlipayObject
    {
        /// <summary>
        /// 对于这个key的描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 对应的字段名
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// 对应的值
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
