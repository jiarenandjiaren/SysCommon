using Newtonsoft.Json;
using System.Collections.Generic;
using Essensoft.AspNetCore.Alipay.Domain;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAppMembersQueryResponse.
    /// </summary>
    public class AlipayOpenAppMembersQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 小程序成员模型
        /// </summary>
        [JsonProperty("app_member_info_list")]
        public List<AppMemberInfo> AppMemberInfoList { get; set; }
    }
}
