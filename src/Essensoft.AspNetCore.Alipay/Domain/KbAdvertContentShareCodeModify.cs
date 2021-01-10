using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// KbAdvertContentShareCodeModify Data Structure.
    /// </summary>
    public class KbAdvertContentShareCodeModify : AlipayObject
    {
        /// <summary>
        /// 宣传展示标题（不能超过30个字符）
        /// </summary>
        [JsonProperty("display_title")]
        public string DisplayTitle { get; set; }
    }
}
