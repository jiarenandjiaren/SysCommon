using Newtonsoft.Json;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayTrustUserScoreGetResponse.
    /// </summary>
    public class AlipayTrustUserScoreGetResponse : AlipayResponse
    {
        /// <summary>
        /// 芝麻信用通过模型计算出的该用户的芝麻信用评分
        /// </summary>
        [JsonProperty("ali_trust_score")]
        public AliTrustScore AliTrustScore { get; set; }
    }
}
