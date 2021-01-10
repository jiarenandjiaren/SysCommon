/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 第三方网站登录验证类
* 类 名： ApiAuth
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
using System;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SysCommon.Service.Interface;

namespace SysCommon.Service.SSO
{
    /// <summary>
    /// 第三方网站登录验证类
    /// </summary>
    public class ApiAuth :IAuth
    {
        private IOptions<AppSetting> _appConfiguration;
        private HttpHelper _helper;
        private IHttpContextAccessor _httpContextAccessor;
        private AuthContextFactory _authContextFactory;


        public ApiAuth(IOptions<AppSetting> appConfiguration
            , IHttpContextAccessor httpContextAccessor
            ,AuthContextFactory authContextFactory
            )
        {
            _appConfiguration = appConfiguration;
            _helper = new HttpHelper(_appConfiguration.Value.SSOPassport);
            _authContextFactory = authContextFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Query[Define.TOKEN_NAME];
            if (!String.IsNullOrEmpty(token)) return token;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies[Define.TOKEN_NAME];
            return cookie == null ? String.Empty : cookie;
        }
        /// <summary>
        /// 通过WebApi检验token是否有效
        /// </summary>
        /// <remarks>http://www.SysCommon.me</remarks>
        public bool CheckLogin(string token="", string otherInfo = "")
        {
            if (string.IsNullOrEmpty(token))
            {
                token = GetToken();
            }

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
         
            var requestUri = String.Format("/api/Check/GetStatus?token={0}&requestid={1}", token, otherInfo);

            try
            {
                var value = _helper.Get(null, requestUri);
                var result = JsonHelper.Instance.Deserialize<Response<bool>>(value);
                if (result.Code == 200)
                {
                    return result.Result;
                }
                throw new Exception(result.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取当前登录的用户信息
        /// <para>通过URL中的Token参数或Cookie中的Token</para>
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>LoginUserVM.</returns>
        public AuthStrategyContext GetCurrentUser()
        {
            string username = GetUserName();
            return _authContextFactory.GetAuthStrategyContext(username);
        }


        /// <summary>
        /// 获取WebApi中当前登录的用户名
        /// <para>通过URL中的Token参数或Cookie中的Token</para>
        /// </summary>
        /// <param name="otherInfo">The account.</param>
        /// <returns>System.String.</returns>
        public string GetUserName(string otherInfo = "")
        {
            var requestUri = String.Format("/api/Check/GetUserName?token={0}&requestid={1}", GetToken(), otherInfo);

            try
            {
                var value = _helper.Get(null, requestUri);
                var result = JsonHelper.Instance.Deserialize<Response<string>>(value);
                if (result.Code == 200)
                {
                    return result.Result;
                }
                throw new Exception(result.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过WebApi登录，用户信息存放在webapi中
        /// </summary>
        /// <param name="appKey">应用程序key.</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>System.String.</returns>
        public LoginResult Login(PassportLoginRequest passportLoginRequest)
        {
            var requestUri = "/api/Check/Login";

            try
            {
                var value = _helper.Post(new
                {
                    //AppKey = appKey,
                    Account = passportLoginRequest.UserName,
                    Password = passportLoginRequest.Password
                }, requestUri);

                var result = JsonHelper.Instance.Deserialize<LoginResult>(value);
                return result;
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        public bool Logout()
        {
            var token = GetToken();
            if (String.IsNullOrEmpty(token)) return true;

            var requestUri = String.Format("/api/Check/Logout?token={0}&requestid={1}", token, "");

            try
            {
                var value = _helper.Post(null, requestUri);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public LoginResult Login(string username, string pwd, string password)
        {
            throw new NotImplementedException();
        }
    }
}