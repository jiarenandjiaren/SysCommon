using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// AlipayUserPicture Data Structure.
    /// </summary>
    public class AlipayUserPicture : AlipayObject
    {
        /// <summary>
        /// 图片类型，包括身份证正反面、营业执照等
        /// </summary>
        [JsonProperty("picture_type")]
        public string PictureType { get; set; }

        /// <summary>
        /// 用于调用alipay.user.certify.image.fetch接口，获取图片资源
        /// </summary>
        [JsonProperty("picture_url")]
        public string PictureUrl { get; set; }
    }
}
