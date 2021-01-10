using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicLifeMsgSendResponse.
    /// </summary>
    public class AlipayOpenPublicLifeMsgSendResponse : AlipayResponse
    {
        /// <summary>
        /// 生活号消息唯一标识
        /// </summary>
        [JsonProperty("alipay_msg_id")]
        public string AlipayMsgId { get; set; }
    }
}
