using Newtonsoft.Json;
using System.Collections.Generic;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdSignatureTaskApplyResponse.
    /// </summary>
    public class AlipaySecurityProdSignatureTaskApplyResponse : AlipayResponse
    {
        /// <summary>
        /// 签约订单id，关联了若干个签约任务id。
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// 签约认证任务列表
        /// </summary>
        [JsonProperty("task_list")]
        public List<SignTaskResult> TaskList { get; set; }
    }
}
