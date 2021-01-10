using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// IdentityParams Data Structure.
    /// </summary>
    public class IdentityParams : AlipayObject
    {
        /// <summary>
        /// 用户身份证号
        /// </summary>
        [JsonProperty("cert_no")]
        public string CertNo { get; set; }

        /// <summary>
        /// 用户实名信息hash值
        /// </summary>
        [JsonProperty("identity_hash")]
        public string IdentityHash { get; set; }

        /// <summary>
        /// 签约指定用户的uid，如用户登录的uid和指定的用户uid不一致则报错
        /// </summary>
        [JsonProperty("sign_user_id")]
        public string SignUserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [JsonProperty("user_name")]
        public string UserName { get; set; }
    }
}
