using Infrastructure;
using SysCommon.Service.Response;

namespace SysCommon.Service.SSO
{
    public class VierificationCodeResult : TableData
    {
        /// <summary>
        /// ��֤��ͼƬ
        /// </summary>
        public string img;
        /// <summary>
        /// ��֤��key
        /// </summary>
        public string UUID;
    }
}