using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayInsSceneApplicationIssueConfirmResponse.
    /// </summary>
    public class AlipayInsSceneApplicationIssueConfirmResponse : AlipayResponse
    {
        /// <summary>
        /// 投保订单号
        /// </summary>
        [JsonProperty("application_no")]
        public string ApplicationNo { get; set; }
    }
}
