using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipaySysCommonIndustryPlatformCreateTokenModel Data Structure.
    /// </summary>
    public class AlipaySysCommonIndustryPlatformCreateTokenModel : AlipayObject
    {
        /// <summary>
        /// isv的appid
        /// </summary>
        [JsonProperty("isv_appid")]
        public string IsvAppid { get; set; }

        /// <summary>
        /// auth_mycar_violation
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
