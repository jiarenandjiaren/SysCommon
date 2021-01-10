using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlisisReportColumn Data Structure.
    /// </summary>
    public class AlisisReportColumn : AlipayObject
    {
        /// <summary>
        /// 列别名
        /// </summary>
        [JsonProperty("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// 列值
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
