using Newtonsoft.Json;

namespace Essensoft.AspNetCore.Alipay.Domain
{
    /// <summary>
    /// FengdieListPaginator Data Structure.
    /// </summary>
    public class FengdieListPaginator : AlipayObject
    {
        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty("page_count")]
        public long PageCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        [JsonProperty("page_number")]
        public long PageNumber { get; set; }

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        [JsonProperty("page_size")]
        public long PageSize { get; set; }

        /// <summary>
        /// 符合条件的记录总数
        /// </summary>
        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
