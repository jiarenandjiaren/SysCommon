using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Response
{
    /// <summary>
    /// AlipayFundAuthOrderUnfreezeResponse.
    /// </summary>
    public class AlipayFundAuthOrderUnfreezeResponse : AlipayResponse
    {
        /// <summary>
        /// 本次操作解冻的金额，单位为：元（人民币），精确到小数点后两位，取值范围：[0.01,100000000.00]
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// 支付宝资金授权订单号
        /// </summary>
        [JsonProperty("auth_no")]
        public string AuthNo { get; set; }

        /// <summary>
        /// 授权资金解冻成功时间，格式：YYYY-MM-DD HH:MM:SS
        /// </summary>
        [JsonProperty("gmt_trans")]
        public string GmtTrans { get; set; }

        /// <summary>
        /// 支付宝资金操作流水号
        /// </summary>
        [JsonProperty("operation_id")]
        public string OperationId { get; set; }

        /// <summary>
        /// 商户的授权资金订单号
        /// </summary>
        [JsonProperty("out_order_no")]
        public string OutOrderNo { get; set; }

        /// <summary>
        /// 商户本次资金操作的请求流水号
        /// </summary>
        [JsonProperty("out_request_no")]
        public string OutRequestNo { get; set; }

        /// <summary>
        /// 资金操作流水的状态  目前支持：  INIT：初始  SUCCESS：成功  CLOSED：关闭
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
