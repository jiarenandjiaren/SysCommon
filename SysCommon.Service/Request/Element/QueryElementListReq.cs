namespace SysCommon.Service.Request
{
    public class QueryElementListReq : PageReq
    {
        public string MenuId { get; set; }

        public QueryElementListReq()
        {
            MenuId = string.Empty;
        }
    }
}
