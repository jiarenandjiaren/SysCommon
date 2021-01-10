using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayOfflineMarketingVoucherStatusQueryModel Data Structure.
    /// </summary>
    public class AlipayOfflineMarketingVoucherStatusQueryModel : AlipayObject
    {
        /// <summary>
        /// 外部流水号
        /// </summary>
        [JsonProperty("out_biz_no")]
        public string OutBizNo { get; set; }

        /// <summary>
        /// 券模板id
        /// </summary>
        [JsonProperty("voucher_id")]
        public string VoucherId { get; set; }
    }
}
