namespace SysCommon.Service.Request
{
    public class QueryArticleDataListReq : PageReq
    {
        /// <summary>
        /// 文章标题
        /// </summary>
       public string Title { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public int Check { get; set; } = 3;
    }
}
