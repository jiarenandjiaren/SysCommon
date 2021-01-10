using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// ProdParams Data Structure.
    /// </summary>
    public class ProdParams : AlipayObject
    {
        /// <summary>
        /// 预授权业务信息
        /// </summary>
        [JsonProperty("auth_biz_params")]
        public string AuthBizParams { get; set; }
    }
}
