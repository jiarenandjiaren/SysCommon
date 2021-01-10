using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayMobilePublicTemplateMessageQueryResponse.
    /// </summary>
    public class AlipayMobilePublicTemplateMessageQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 结果值
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
