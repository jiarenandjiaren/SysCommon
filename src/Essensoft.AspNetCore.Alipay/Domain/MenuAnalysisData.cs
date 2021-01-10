using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// MenuAnalysisData Data Structure.
    /// </summary>
    public class MenuAnalysisData : AlipayObject
    {
        /// <summary>
        /// 人均点击次数
        /// </summary>
        [JsonProperty("avg_click_user_cnt")]
        public long AvgClickUserCnt { get; set; }

        /// <summary>
        /// 菜单点击次数
        /// </summary>
        [JsonProperty("click_cnt")]
        public long ClickCnt { get; set; }

        /// <summary>
        /// 菜单点击人数
        /// </summary>
        [JsonProperty("click_user_cnt")]
        public long ClickUserCnt { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// 菜单类型 ，iconDefault ：图标菜单、default：文字菜单
        /// </summary>
        [JsonProperty("menu_type")]
        public string MenuType { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 子菜单名称，文字菜单才有
        /// </summary>
        [JsonProperty("sub_name")]
        public string SubName { get; set; }
    }
}
