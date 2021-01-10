using Newtonsoft.Json;
using System.Collections.Generic;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// KoubeiItemExtitemBrandQueryResponse.
    /// </summary>
    public class KoubeiItemExtitemBrandQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 品牌列表信息
        /// </summary>
        [JsonProperty("brand_list")]
        public List<ExtBrand> BrandList { get; set; }
    }
}
