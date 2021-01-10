/**  �汾��Ϣģ���ڰ�װĿ¼�£��������޸ġ�
*
*
* �� �ܣ� ��������վ��¼��֤��
* �� ���� ApiAuth
*
* Ver    �������             ������ GYJ  �������
* ����������������������������������������������������������������������
* V0.01  2020/9/18 9:25   N/A    ����
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*������������������������������������������������������������������������
*�����˼�����ϢΪ����˾������Ϣ��δ������˾����ͬ���ֹ���������¶������
*������Ȩ���У��Ų��Ƽ����޹�˾����������������������������            ��
*������������������������������������������������������������������������
*/
using System;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SysCommon.Service.Interface;

namespace SysCommon.Service.SSO
{
    /// <summary>
    /// ��������վ��¼��֤��
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
        /// ͨ��WebApi����token�Ƿ���Ч
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
        /// ��ȡ��ǰ��¼���û���Ϣ
        /// <para>ͨ��URL�е�Token������Cookie�е�Token</para>
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>LoginUserVM.</returns>
        public AuthStrategyContext GetCurrentUser()
        {
            string username = GetUserName();
            return _authContextFactory.GetAuthStrategyContext(username);
        }


        /// <summary>
        /// ��ȡWebApi�е�ǰ��¼���û���
        /// <para>ͨ��URL�е�Token������Cookie�е�Token</para>
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
        /// ͨ��WebApi��¼���û���Ϣ�����webapi��
        /// </summary>
        /// <param name="appKey">Ӧ�ó���key.</param>
        /// <param name="username">�û���</param>
        /// <param name="pwd">����</param>
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
        /// ע��
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