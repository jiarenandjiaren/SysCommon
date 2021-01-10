using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayEcoMycarDataserviceViolationinfoShareResponse.
    /// </summary>
    public class AlipayEcoMycarDataserviceViolationinfoShareResponse : AlipayResponse
    {
        /// <summary>
        /// 车架号
        /// </summary>
        [JsonProperty("body_num")]
        public string BodyNum { get; set; }

        /// <summary>
        /// 发动机号
        /// </summary>
        [JsonProperty("engine_num")]
        public string EngineNum { get; set; }

        /// <summary>
        /// 车辆id
        /// </summary>
        [JsonProperty("vehicle_id")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        [JsonProperty("vi_number")]
        public string ViNumber { get; set; }
    }
}
