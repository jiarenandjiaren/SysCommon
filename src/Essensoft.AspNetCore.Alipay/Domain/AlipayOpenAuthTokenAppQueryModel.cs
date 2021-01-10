using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipaySysCommonTokenAppQueryModel Data Structure.
    /// </summary>
    public class AlipaySysCommonTokenAppQueryModel : AlipayObject
    {
        /// <summary>
        /// 应用授权令牌
        /// </summary>
        [JsonProperty("app_auth_token")]
        public string AppAuthToken { get; set; }
    }
}
