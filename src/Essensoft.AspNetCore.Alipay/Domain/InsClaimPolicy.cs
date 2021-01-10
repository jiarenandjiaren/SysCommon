using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// InsClaimPolicy Data Structure.
    /// </summary>
    public class InsClaimPolicy : AlipayObject
    {
        /// <summary>
        /// 保单号
        /// </summary>
        [JsonProperty("policy_no")]
        public string PolicyNo { get; set; }
    }
}
