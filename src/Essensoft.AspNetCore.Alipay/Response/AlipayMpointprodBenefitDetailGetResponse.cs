using Newtonsoft.Json;
using System.Collections.Generic;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayMpointprodBenefitDetailGetResponse.
    /// </summary>
    public class AlipayMpointprodBenefitDetailGetResponse : AlipayResponse
    {
        /// <summary>
        /// 权益详情列表
        /// </summary>
        [JsonProperty("benefit_infos")]
        public List<BenefitInfo> BenefitInfos { get; set; }

        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 响应描述
        /// </summary>
        [JsonProperty("msg")]
        public new string Msg { get; set; }
    }
}
