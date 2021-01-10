using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// Topic Data Structure.
    /// </summary>
    public class Topic : AlipayObject
    {
        /// <summary>
        /// 营销位图片url
        /// </summary>
        [JsonProperty("img_url")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 营销位跳转地址，点击营销位头图跳到的链接url。
        /// </summary>
        [JsonProperty("link_url")]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 营销位描述
        /// </summary>
        [JsonProperty("sub_title")]
        public string SubTitle { get; set; }

        /// <summary>
        /// 营销位名称
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 营销位id
        /// </summary>
        [JsonProperty("topic_id")]
        public string TopicId { get; set; }

        /// <summary>
        /// 营销位内容列表
        /// </summary>
        [JsonProperty("topic_items")]
        public List<TopicItem> TopicItems { get; set; }
    }
}
