using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCampaignDiscountBudgetQueryModel Data Structure.
    /// </summary>
    public class AlipayMarketingCampaignDiscountBudgetQueryModel : AlipayObject
    {
        /// <summary>
        /// 预算名称
        /// </summary>
        [JsonProperty("budget_id")]
        public string BudgetId { get; set; }
    }
}
