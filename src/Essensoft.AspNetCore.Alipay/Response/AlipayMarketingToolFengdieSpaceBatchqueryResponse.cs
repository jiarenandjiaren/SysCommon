using Newtonsoft.Json;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieSpaceBatchqueryResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieSpaceBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 云凤蝶空间列表查询接口返回值，包含分页信息，空间信息的列表。
        /// </summary>
        [JsonProperty("data")]
        public FengdieSpaceListRespModel Data { get; set; }
    }
}
