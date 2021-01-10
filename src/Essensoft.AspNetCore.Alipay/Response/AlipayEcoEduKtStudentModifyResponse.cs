using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayEcoEduKtStudentModifyResponse.
    /// </summary>
    public class AlipayEcoEduKtStudentModifyResponse : AlipayResponse
    {
        /// <summary>
        /// Y：代表成功；N：代表失败
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
