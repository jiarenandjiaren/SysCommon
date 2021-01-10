using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// KbAdvertContentPassword Data Structure.
    /// </summary>
    public class KbAdvertContentPassword : AlipayObject
    {
        /// <summary>
        /// 红包口令
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// 红包口令分享地址
        /// </summary>
        [JsonProperty("share_page_url")]
        public string SharePageUrl { get; set; }
    }
}
