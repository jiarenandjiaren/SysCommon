using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdFacerepoAddResponse.
    /// </summary>
    public class AlipaySecurityProdFacerepoAddResponse : AlipayResponse
    {
        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty("ext_info")]
        public string ExtInfo { get; set; }

        /// <summary>
        /// 对此次插入人脸库分组的人脸id标识
        /// </summary>
        [JsonProperty("face_id")]
        public string FaceId { get; set; }
    }
}
