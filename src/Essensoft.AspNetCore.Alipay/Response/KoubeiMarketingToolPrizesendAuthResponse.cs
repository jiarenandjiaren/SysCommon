using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingToolPrizesendAuthResponse.
    /// </summary>
    public class KoubeiMarketingToolPrizesendAuthResponse : AlipayResponse
    {
        /// <summary>
        /// 发奖token，用于校验商户是否有权限给制定用户发奖
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
