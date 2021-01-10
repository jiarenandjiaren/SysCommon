using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdSignatureTaskCancelResponse.
    /// </summary>
    public class AlipaySecurityProdSignatureTaskCancelResponse : AlipayResponse
    {
        /// <summary>
        /// 是否更新成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
