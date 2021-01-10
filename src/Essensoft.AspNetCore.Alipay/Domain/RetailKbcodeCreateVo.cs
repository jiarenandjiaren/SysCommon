using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// RetailKbcodeCreateVo Data Structure.
    /// </summary>
    public class RetailKbcodeCreateVo : AlipayObject
    {
        /// <summary>
        /// 指定码图片上显示的名称，建议使用商户名称
        /// </summary>
        [JsonProperty("code_tip")]
        public string CodeTip { get; set; }

        /// <summary>
        /// 销售员信息（只允许数字和字母），建议使用促销员手机号
        /// </summary>
        [JsonProperty("salesman")]
        public string Salesman { get; set; }
    }
}
