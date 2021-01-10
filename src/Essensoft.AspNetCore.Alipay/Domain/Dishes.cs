using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// Dishes Data Structure.
    /// </summary>
    public class Dishes : AlipayObject
    {
        /// <summary>
        /// 外部菜品ID  当dish_list[]不为空的时候，dish_id是必填字段。
        /// </summary>
        [JsonProperty("dish_id")]
        public string DishId { get; set; }

        /// <summary>
        /// 菜品名称
        /// </summary>
        [JsonProperty("dish_name")]
        public string DishName { get; set; }

        /// <summary>
        /// 菜品数量
        /// </summary>
        [JsonProperty("dish_num")]
        public string DishNum { get; set; }

        /// <summary>
        /// 菜品价格（单位分）
        /// </summary>
        [JsonProperty("dish_price")]
        public string DishPrice { get; set; }

        /// <summary>
        /// 菜品折后价格（单位分）
        /// </summary>
        [JsonProperty("dish_real_price")]
        public string DishRealPrice { get; set; }

        /// <summary>
        /// 1234
        /// </summary>
        [JsonProperty("dish_skuid")]
        public string DishSkuid { get; set; }
    }
}
