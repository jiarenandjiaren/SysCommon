namespace SysCommon.Service.Request
{
    public class QuerySysUserListReq :PageReq
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
}
