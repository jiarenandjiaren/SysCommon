using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayZdatafrontDatatransferedSendResponse.
    /// </summary>
    public class AlipayZdatafrontDatatransferedSendResponse : AlipayResponse
    {
        /// <summary>
        /// 表示数据传输是否成功
        /// </summary>
        [JsonProperty("success")]
        public string Success { get; set; }
    }
}
