using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// TemplateMdcodeNotifyConfDTO Data Structure.
    /// </summary>
    public class TemplateMdcodeNotifyConfDTO : AlipayObject
    {
        /// <summary>
        /// 扩展参数信息；  格式为key-value键值对；  支付宝POST请求指定url时，除BizCardNo等固定参数外，将带上ext_params中配置的所有key-value参数。
        /// </summary>
        [JsonProperty("ext_params")]
        public string ExtParams { get; set; }

        /// <summary>
        /// 商户接收发码通知的地址链接；  只支持https地址；  用户打开会员卡时，支付宝提交POST请求此url地址，通知商户发码。
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
