using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayEcoEduKtSchoolinfoModifyResponse.
    /// </summary>
    public class AlipayEcoEduKtSchoolinfoModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝分配给学校的编码，作为学校在支付宝的标识
        /// </summary>
        [JsonProperty("school_no")]
        public string SchoolNo { get; set; }

        /// <summary>
        /// Y：代表成功；N：代表失败
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
