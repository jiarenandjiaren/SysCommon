using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// KoubeiCateringEleOrderSyncModel Data Structure.
    /// </summary>
    public class KoubeiCateringEleOrderSyncModel : AlipayObject
    {
        /// <summary>
        /// 饿了么数据回流action类型.包含ORDER_STATUS，ORDER_DELIVERY,ORDER_REFUND
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        /// <summary>
        /// 支付宝用户id
        /// </summary>
        [JsonProperty("alipay_user_id")]
        public string AlipayUserId { get; set; }

        /// <summary>
        /// 饿了么推送的数据类型json字符串.注意data是一个字符串类型,要把一串json作为字符串传入
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// 数据推送来源,需要找业务PD申请
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }
    }
}
