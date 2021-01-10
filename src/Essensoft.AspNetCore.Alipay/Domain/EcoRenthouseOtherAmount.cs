using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// EcoRenthouseOtherAmount Data Structure.
    /// </summary>
    public class EcoRenthouseOtherAmount : AlipayObject
    {
        /// <summary>
        /// 30
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// 费用名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 费用单位
        /// </summary>
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
}
