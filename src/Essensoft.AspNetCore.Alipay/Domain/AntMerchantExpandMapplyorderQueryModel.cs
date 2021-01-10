using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandMapplyorderQueryModel Data Structure.
    /// </summary>
    public class AntMerchantExpandMapplyorderQueryModel : AlipayObject
    {
        /// <summary>
        /// 支付宝端商户入驻申请单据号
        /// </summary>
        [JsonProperty("order_no")]
        public string OrderNo { get; set; }
    }
}
