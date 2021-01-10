using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// StagedDiscountDstCampPrizeModel Data Structure.
    /// </summary>
    public class StagedDiscountDstCampPrizeModel : AlipayObject
    {
        /// <summary>
        /// 折扣预算ID
        /// </summary>
        [JsonProperty("budget_id")]
        public string BudgetId { get; set; }

        /// <summary>
        /// 折扣幅度列表.
        /// </summary>
        [JsonProperty("discount_rate_model_list")]
        public List<DiscountRateModel> DiscountRateModelList { get; set; }

        /// <summary>
        /// 奖品id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 单次优惠上限(元)
        /// </summary>
        [JsonProperty("max_discount_amt")]
        public string MaxDiscountAmt { get; set; }
    }
}
