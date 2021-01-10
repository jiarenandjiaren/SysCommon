using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayUserTestResponse.
    /// </summary>
    public class AlipayUserTestResponse : AlipayResponse
    {
        /// <summary>
        /// 返回值
        /// </summary>
        [JsonProperty("ret1")]
        public string Ret1 { get; set; }
    }
}
