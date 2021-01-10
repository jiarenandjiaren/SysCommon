using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenPublicLifeLabelDeleteModel Data Structure.
    /// </summary>
    public class AlipayOpenPublicLifeLabelDeleteModel : AlipayObject
    {
        /// <summary>
        /// 标签id, 只支持生活号自定义标签
        /// </summary>
        [JsonProperty("label_id")]
        public string LabelId { get; set; }
    }
}
