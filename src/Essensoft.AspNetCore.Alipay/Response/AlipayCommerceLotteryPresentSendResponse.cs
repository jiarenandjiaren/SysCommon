using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceLotteryPresentSendResponse.
    /// </summary>
    public class AlipayCommerceLotteryPresentSendResponse : AlipayResponse
    {
        /// <summary>
        /// 是否赠送成功
        /// </summary>
        [JsonProperty("send_result")]
        public bool SendResult { get; set; }
    }
}
