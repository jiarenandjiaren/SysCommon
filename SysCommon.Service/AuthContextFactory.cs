/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 加载用户所有可访问的资源/机构/模块
* 类 名： AuthContextFactory
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/18 9:25   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/


using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.Service
{
    /// <summary>
    /// 加载用户所有可访问的资源/机构/模块
    /// </summary>
    public class AuthContextFactory
    {
        private SystemAuthStrategy _systemAuth;//系统管理员登录
        private NormalAuthStrategy _normalAuthStrategy;//普通用户登录
        private readonly IUnitWork _unitWork;

        public AuthContextFactory(SystemAuthStrategy sysStrategy
           , NormalAuthStrategy normalAuthStrategy
           , IUnitWork unitWork)
        {
            _systemAuth = sysStrategy;
            _normalAuthStrategy = normalAuthStrategy;
            _unitWork = unitWork;
        }

        public AuthStrategyContext GetAuthStrategyContext(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;

            IAuthStrategy service = null;
            if (username == Define.SYSTEM_USERNAME)
            {
                service = _systemAuth;
            }
            else
            {
                service = _normalAuthStrategy;
                service.User = _unitWork.FindSingle<SysUser>(u => u.UserName == username);
            }

            return new AuthStrategyContext(service);
        }
    }
}