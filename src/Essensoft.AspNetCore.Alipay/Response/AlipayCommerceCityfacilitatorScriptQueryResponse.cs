using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceCityfacilitatorScriptQueryResponse.
    /// </summary>
    public class AlipayCommerceCityfacilitatorScriptQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 脚本内容，智能卡中心自定义的脚本格式内容
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// 脚本更新时间
        /// </summary>
        [JsonProperty("gmt_modified")]
        public string GmtModified { get; set; }
    }
}
