using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayMobilePublicShortlinkCreateResponse.
    /// </summary>
    public class AlipayMobilePublicShortlinkCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 结果码
        /// </summary>
        [JsonProperty("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        [JsonProperty("msg")]
        public new string Msg { get; set; }

        /// <summary>
        /// 短链接url
        /// </summary>
        [JsonProperty("shortlink")]
        public string Shortlink { get; set; }
    }
}
