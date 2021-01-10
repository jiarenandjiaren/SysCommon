using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenPublicTopicCreateModel Data Structure.
    /// </summary>
    public class AlipayOpenPublicTopicCreateModel : AlipayObject
    {
        /// <summary>
        /// 营销位图片url, 尺寸为996*450，最大不超过5M，支持格式:.jpg、.png ，请先调用<a href="https://docs.open.alipay.com/api_3/alipay.offline.material.image.upload"> 图片上传接口</a>获得图片url。营销位需要展示头图时，必须填写该参数。
        /// </summary>
        [JsonProperty("img_url")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 营销位跳转地址，点击营销位头图跳到的链接url。营销位需要展示头图时，必须填写该参数。
        /// </summary>
        [JsonProperty("link_url")]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 营销位描述。营销位需要展示头图时，必须填写该参数。
        /// </summary>
        [JsonProperty("sub_title")]
        public string SubTitle { get; set; }

        /// <summary>
        /// 营销位名称
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 营销位内容，数量限制：大于4个，小于8个
        /// </summary>
        [JsonProperty("topic_items")]
        public List<TopicItem> TopicItems { get; set; }
    }
}
