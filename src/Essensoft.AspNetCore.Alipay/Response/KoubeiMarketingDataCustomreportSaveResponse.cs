using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataCustomreportSaveResponse.
    /// </summary>
    public class KoubeiMarketingDataCustomreportSaveResponse : AlipayResponse
    {
        /// <summary>
        /// 自定义报表的规则ID
        /// </summary>
        [JsonProperty("condition_key")]
        public string ConditionKey { get; set; }
    }
}
