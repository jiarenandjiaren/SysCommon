using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayAssetPointBalanceQueryResponse.
    /// </summary>
    public class AlipayAssetPointBalanceQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 用户的集分宝余额
        /// </summary>
        [JsonProperty("point_amount")]
        public long PointAmount { get; set; }
    }
}
