using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayEcoMycarParkingVehicleQueryResponse.
    /// </summary>
    public class AlipayEcoMycarParkingVehicleQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 车牌信息（utf-8编码）
        /// </summary>
        [JsonProperty("car_number")]
        public string CarNumber { get; set; }
    }
}
