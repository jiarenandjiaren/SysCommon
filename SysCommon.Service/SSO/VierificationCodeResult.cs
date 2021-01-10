using Infrastructure;
using SysCommon.Service.Response;

namespace SysCommon.Service.SSO
{
    public class VierificationCodeResult : TableData
    {
        /// <summary>
        /// 验证码图片
        /// </summary>
        public string img;
        /// <summary>
        /// 验证码key
        /// </summary>
        public string UUID;
    }
}