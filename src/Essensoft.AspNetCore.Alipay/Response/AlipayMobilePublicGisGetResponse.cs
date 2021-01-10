using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayMobilePublicGisGetResponse.
    /// </summary>
    public class AlipayMobilePublicGisGetResponse : AlipayResponse
    {
        /// <summary>
        /// 精确度
        /// </summary>
        [JsonProperty("accuracy")]
        public string Accuracy { get; set; }

        /// <summary>
        /// 经纬度所在位置
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 结果码
        /// </summary>
        [JsonProperty("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 纬度信息
        /// </summary>
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// 经度信息
        /// </summary>
        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        [JsonProperty("msg")]
        public new string Msg { get; set; }

        /// <summary>
        /// 经纬度对应位置所在的省份
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }
    }
}
