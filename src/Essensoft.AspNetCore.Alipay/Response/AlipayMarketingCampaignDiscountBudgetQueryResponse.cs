using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignDiscountBudgetQueryResponse.
    /// </summary>
    public class AlipayMarketingCampaignDiscountBudgetQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 预算ID
        /// </summary>
        [JsonProperty("budget_id")]
        public string BudgetId { get; set; }

        /// <summary>
        /// 预算剩余冻结金额(元)
        /// </summary>
        [JsonProperty("freeze_amount")]
        public string FreezeAmount { get; set; }

        /// <summary>
        /// 预算已回收退回保证金账户金额(元)
        /// </summary>
        [JsonProperty("recycle_amount")]
        public string RecycleAmount { get; set; }

        /// <summary>
        /// 交易已退款金额(元)
        /// </summary>
        [JsonProperty("refund_amount")]
        public string RefundAmount { get; set; }

        /// <summary>
        /// 预算总金额(元)
        /// </summary>
        [JsonProperty("total_amount")]
        public string TotalAmount { get; set; }

        /// <summary>
        /// 交易已使用金额(元)
        /// </summary>
        [JsonProperty("used_amount")]
        public string UsedAmount { get; set; }
    }
}
