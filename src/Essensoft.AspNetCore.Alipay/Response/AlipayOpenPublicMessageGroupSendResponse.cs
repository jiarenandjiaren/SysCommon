using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicMessageGroupSendResponse.
    /// </summary>
    public class AlipayOpenPublicMessageGroupSendResponse : AlipayResponse
    {
        /// <summary>
        /// 2013121100055554f1000b64-3f05-4581-a5e1-16e29d242950
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}
