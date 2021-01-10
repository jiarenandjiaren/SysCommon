using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// GroupSetting Data Structure.
    /// </summary>
    public class GroupSetting : AlipayObject
    {
        /// <summary>
        /// 群名称
        /// </summary>
        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        /// <summary>
        /// 是否开放群成员邀请
        /// </summary>
        [JsonProperty("is_openinv")]
        public bool IsOpeninv { get; set; }

        /// <summary>
        /// 群公告
        /// </summary>
        [JsonProperty("public_notice")]
        public string PublicNotice { get; set; }
    }
}
