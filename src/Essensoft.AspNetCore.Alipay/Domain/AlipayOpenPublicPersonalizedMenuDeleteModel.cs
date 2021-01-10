using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenPublicPersonalizedMenuDeleteModel Data Structure.
    /// </summary>
    public class AlipayOpenPublicPersonalizedMenuDeleteModel : AlipayObject
    {
        /// <summary>
        /// 要删除的个性化菜单key
        /// </summary>
        [JsonProperty("menu_key")]
        public string MenuKey { get; set; }
    }
}
