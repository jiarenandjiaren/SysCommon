using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicSettingCategoryQueryResponse.
    /// </summary>
    public class AlipayOpenPublicSettingCategoryQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回已设置的一级行业分类名称
        /// </summary>
        [JsonProperty("primary_category")]
        public string PrimaryCategory { get; set; }

        /// <summary>
        /// 返回已设置的二级行业分类名称
        /// </summary>
        [JsonProperty("secondary_category")]
        public string SecondaryCategory { get; set; }
    }
}
