using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipaySysCommonIndustryPlatformCreateTokenResponse.
    /// </summary>
    public class AlipaySysCommonIndustryPlatformCreateTokenResponse : AlipayResponse
    {
        /// <summary>
        /// 授权码
        /// </summary>
        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }

        /// <summary>
        /// appid
        /// </summary>
        [JsonProperty("requst_app_id")]
        public string RequstAppId { get; set; }

        /// <summary>
        /// scope
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
