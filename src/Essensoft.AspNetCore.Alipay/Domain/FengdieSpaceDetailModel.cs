using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// FengdieSpaceDetailModel Data Structure.
    /// </summary>
    public class FengdieSpaceDetailModel : AlipayObject
    {
        /// <summary>
        /// 空间可用域名列表，云凤蝶站点发布后链接可用采用的域名
        /// </summary>
        [JsonProperty("domains")]
        public List<FengdieSpaceDomains> Domains { get; set; }

        /// <summary>
        /// 空间创建时间
        /// </summary>
        [JsonProperty("gmt_create")]
        public string GmtCreate { get; set; }

        /// <summary>
        /// 空间 ID
        /// </summary>
        [JsonProperty("space_id")]
        public string SpaceId { get; set; }

        /// <summary>
        /// 空间标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
