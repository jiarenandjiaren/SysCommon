using System;

namespace SysCommon.Service.SSO
{
    [Serializable]
    public class UserAuthSession
    {
        public string Token { get; set; }

        //public string AppKey { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        public string Id { get; set; }
        
        public string IpAddress { get; set; }

        public DateTime CreateTime { get; set; }
        public int LoginCount { get; set; }
    }
}