using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// TableListResult Data Structure.
    /// </summary>
    public class TableListResult : AlipayObject
    {
        /// <summary>
        /// 桌名
        /// </summary>
        [JsonProperty("table_name")]
        public string TableName { get; set; }

        /// <summary>
        /// 桌号
        /// </summary>
        [JsonProperty("table_num")]
        public string TableNum { get; set; }
    }
}
