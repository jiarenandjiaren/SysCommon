using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCardBenefitQueryModel Data Structure.
    /// </summary>
    public class AlipayMarketingCardBenefitQueryModel : AlipayObject
    {
        /// <summary>
        /// 权益ID
        /// </summary>
        [JsonProperty("benefit_id")]
        public string BenefitId { get; set; }

        /// <summary>
        /// 会员卡模板ID
        /// </summary>
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }
    }
}
