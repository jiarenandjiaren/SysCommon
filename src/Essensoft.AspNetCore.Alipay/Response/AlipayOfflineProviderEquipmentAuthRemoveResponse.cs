using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayOfflineProviderEquipmentAuthRemoveResponse.
    /// </summary>
    public class AlipayOfflineProviderEquipmentAuthRemoveResponse : AlipayResponse
    {
        /// <summary>
        /// 被解绑的机具编号
        /// </summary>
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// 机具厂商PID
        /// </summary>
        [JsonProperty("merchant_pid")]
        public string MerchantPid { get; set; }
    }
}
