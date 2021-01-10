namespace SysCommon.Service.Request
{
    public class QueryArticleTypeListReq : PageReq
    {
        public QueryArticleTypeListReq()
        {
            this.TypeName = string.Empty;
            this.IsEnable = 2;
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 状态（1：在用 0：停用 2:全部）
        /// </summary>
        public int? IsEnable { get; set; } = 2;
    }
}
