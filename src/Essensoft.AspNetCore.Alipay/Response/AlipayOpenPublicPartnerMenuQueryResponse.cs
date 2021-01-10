using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicPartnerMenuQueryResponse.
    /// </summary>
    public class AlipayOpenPublicPartnerMenuQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 服务窗菜单
        /// </summary>
        [JsonProperty("public_menu")]
        public string PublicMenu { get; set; }
    }
}
