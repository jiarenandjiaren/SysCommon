using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayInsCooperationProductQrcodeApplyResponse.
    /// </summary>
    public class AlipayInsCooperationProductQrcodeApplyResponse : AlipayResponse
    {
        /// <summary>
        /// 快捷投保产品二维码图片链接URL
        /// </summary>
        [JsonProperty("code_url")]
        public string CodeUrl { get; set; }
    }
}
