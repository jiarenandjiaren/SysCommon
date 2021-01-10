using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayInsAutoAutoinsprodCommonConsultResponse.
    /// </summary>
    public class AlipayInsAutoAutoinsprodCommonConsultResponse : AlipayResponse
    {
        /// <summary>
        /// 具体内容按照业务类型对应的key值传输
        /// </summary>
        [JsonProperty("biz_data")]
        public string BizData { get; set; }
    }
}
