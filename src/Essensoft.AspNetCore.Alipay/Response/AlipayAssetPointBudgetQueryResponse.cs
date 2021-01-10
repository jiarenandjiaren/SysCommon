using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayAssetPointBudgetQueryResponse.
    /// </summary>
    public class AlipayAssetPointBudgetQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 还可以发放的集分宝个数
        /// </summary>
        [JsonProperty("budget_amount")]
        public long BudgetAmount { get; set; }
    }
}
