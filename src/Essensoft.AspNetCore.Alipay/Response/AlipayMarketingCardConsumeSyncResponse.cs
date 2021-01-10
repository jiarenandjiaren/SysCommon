using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCardConsumeSyncResponse.
    /// </summary>
    public class AlipayMarketingCardConsumeSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 外部卡号
        /// </summary>
        [JsonProperty("external_card_no")]
        public string ExternalCardNo { get; set; }
    }
}
