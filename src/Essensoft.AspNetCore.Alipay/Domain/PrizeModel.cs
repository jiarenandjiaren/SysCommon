using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// PrizeModel Data Structure.
    /// </summary>
    public class PrizeModel : AlipayObject
    {
        /// <summary>
        /// 生效时间
        /// </summary>
        [JsonProperty("active_time")]
        public string ActiveTime { get; set; }

        /// <summary>
        /// 可用金额，单位元，精度分
        /// </summary>
        [JsonProperty("available_amount")]
        public string AvailableAmount { get; set; }

        /// <summary>
        /// 可用次数，大于1为可找零红包，等于1为不找零红包
        /// </summary>
        [JsonProperty("available_count")]
        public long AvailableCount { get; set; }

        /// <summary>
        /// 奖品描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 折扣，包含分期信息
        /// </summary>
        [JsonProperty("discount_list")]
        public List<DiscountModel> DiscountList { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        [JsonProperty("expired_time")]
        public string ExpiredTime { get; set; }

        /// <summary>
        /// 扩展信息，JSON结构
        /// </summary>
        [JsonProperty("ext_info")]
        public string ExtInfo { get; set; }

        /// <summary>
        /// 奖品名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 奖品的复合ID
        /// </summary>
        [JsonProperty("prize_id")]
        public string PrizeId { get; set; }

        /// <summary>
        /// 奖品状态  VALID 可使用  EXPIRED 已过期  ALL_USED 全部使用完
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 总金额，单位元，精度分
        /// </summary>
        [JsonProperty("total_amount")]
        public string TotalAmount { get; set; }

        /// <summary>
        /// 奖品类型  DISCOUNT_VOUCHER 利率打折卡券  COUPON_VOUCHER 利息红包卡券
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 已使用次数
        /// </summary>
        [JsonProperty("used_count")]
        public long UsedCount { get; set; }
    }
}
