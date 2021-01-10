using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandContractFacetofaceSignResponse.
    /// </summary>
    public class AntMerchantExpandContractFacetofaceSignResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝端商户入驻申请单据号
        /// </summary>
        [JsonProperty("order_no")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 外部入驻申请单据号
        /// </summary>
        [JsonProperty("out_biz_no")]
        public string OutBizNo { get; set; }
    }
}
