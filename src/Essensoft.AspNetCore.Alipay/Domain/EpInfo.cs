using Newtonsoft.Json;
using System.Collections.Generic;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// EpInfo Data Structure.
    /// </summary>
    public class EpInfo : AlipayObject
    {
        /// <summary>
        /// 企业征信元素列表，如一套企业工商照面信息。数据长度不定。
        /// </summary>
        [JsonProperty("ep_element_list")]
        public List<EpElement> EpElementList { get; set; }
    }
}
