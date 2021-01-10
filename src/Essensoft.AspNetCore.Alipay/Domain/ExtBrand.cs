using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// ExtBrand Data Structure.
    /// </summary>
    public class ExtBrand : AlipayObject
    {
        /// <summary>
        /// 品牌编码
        /// </summary>
        [JsonProperty("brand_code")]
        public string BrandCode { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
