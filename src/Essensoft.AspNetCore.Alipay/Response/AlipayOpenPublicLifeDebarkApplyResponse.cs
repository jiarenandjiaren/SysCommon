using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicLifeDebarkApplyResponse.
    /// </summary>
    public class AlipayOpenPublicLifeDebarkApplyResponse : AlipayResponse
    {
        /// <summary>
        /// 下架成功后返回的提示
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
