namespace SysCommon.Service.Request
{
    public class QueryLabelListReq : PageReq
    {
        public QueryLabelListReq()
        {
            this.LabelName = string.Empty;
        }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName { get; set; }
        /// <summary>
        /// 状态（1：在用 0：停用 2:全部）
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 类型id
        /// </summary>
        public string TypeId { get; set; }
    }
}
