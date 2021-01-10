/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 登录解析
*         处理登录逻辑，验证客户段提交的账号密码，保存登录信息
* 类 名： LoginParse
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/18 9:18   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using Infrastructure.Cache;
using Microsoft.Extensions.Caching.Memory;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;
using Infrastructure.Extensions;
using Infrastructure.Utilities;
using SysCommon.Service.Enum;

namespace SysCommon.Service.SSO
{
    public class LoginParse
    {

        //这个地方使用IRepository<User> 而不使用UserManagerApp是防止循环依赖
        public IRepository<SysUser> _app;
        private ICacheContext _cacheContext;
        private AppInfoService _appInfoService;
        private readonly AdminService _adminService;

        public LoginParse( AppInfoService infoService, ICacheContext cacheContext, IRepository<SysUser> userApp, AdminService adminService)
        {
            _appInfoService = infoService;
            _cacheContext = cacheContext;
            _app = userApp;
            _adminService = adminService;
        }

        public  LoginResult Do(PassportLoginRequest model)
        {
            var result = new LoginResult();
            try
            {
                //model.Trim();


                //获取应用信息
                //var appInfo = _appInfoService.Get(model.ServiceKey);
                //if (appInfo == null)
                //{
                //    throw  new Exception("应用不存在");
                //}
                //获取用户信息
                SysUser userInfo = null;
                if (model.UserName == Define.SYSTEM_USERNAME)
                {
                    userInfo = new SysUser
                    {
                        Id = Guid.Empty.ToString(), 
                        //Account = Define.SYSTEM_USERNAME,
                        UserName = Define.SYSTEM_USERNAME,
                        Password = Define.SYSTEM_USERPWD
                    };
                }
                else
                {
                    userInfo = _app.FindSingle(u =>u.UserName == model.UserName);
                }
                if (userInfo == null)
                {
                    throw new Exception("用户不存在");
                }
                //IMemoryCache memoryCache = HttpContext.Current.GetService<IMemoryCache>();
                //string cacheCode = (memoryCache.Get(model.Uuid) ?? "").ToString();
                //if (string.IsNullOrEmpty(cacheCode))
                //{
                //    throw new Exception("验证码错误，请刷新重试！");
                //}
                //if (cacheCode.ToLower() != model.VerificationCode.ToLower())
                //{
                //    memoryCache.Remove(model.Uuid);
                //    throw new Exception("验证码不正确！");
                //}
                if (userInfo.IsDelete)
                {
                    throw new Exception("该用户名已经被删除，需要请联系管理员！");
                }
                if (!userInfo.IsEnable)
                {
                    throw new Exception("该用户名已经停用，需要请联系管理员！");
                }
                if (userInfo.Password != model.Password)
                {
                    throw new Exception("密码错误");
                }

                var currentSession = new UserAuthSession
                {
                    Id = userInfo.Id,
                    Name = userInfo.UserName,
                    LoginCount = userInfo.LoginCount + 1,
                    Token = Guid.NewGuid().ToString().GetHashCode().ToString("x"),
                    //AppKey = model.ServiceKey,
                    CreateTime = DateTime.Now
               //    , IpAddress = HttpContext.Current.Request.UserHostAddress
                 
                };
                _adminService.Count(currentSession.LoginCount, currentSession.Name);
                //创建Session
                _cacheContext.Set(currentSession.Token, currentSession, DateTime.Now.AddHours(Define. ExpHours));

                result.Code = 100;
                //result.ReturnUrl = appInfo.ReturnUrl;
                //result.Token = currentSession.Token;
                result.Data = currentSession;
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}