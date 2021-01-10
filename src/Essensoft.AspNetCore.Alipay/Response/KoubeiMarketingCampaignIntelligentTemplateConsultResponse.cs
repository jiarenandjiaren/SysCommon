using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignIntelligentTemplateConsultResponse.
    /// </summary>
    public class KoubeiMarketingCampaignIntelligentTemplateConsultResponse : AlipayResponse
    {
        /// <summary>
        /// 营销模板的编号  GENERAL_NORMAL：全场普通；  ITEM_NORMAL：单品普通;  CROWD_NORMAL: 千人千券普通；
        /// </summary>
        [JsonProperty("template_codes")]
        public List<string> TemplateCodes { get; set; }
    }
}
