/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 登录及与登录信息获取相关的接口（权限分配）
* 类 名： CheckController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:51   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/

using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysCommon.Service;
using SysCommon.Service.Interface;
using SysCommon.Service.Response;
using SysCommon.Service.SSO;
using System;
using Microsoft.Extensions.Logging;
using SysCommon.Service.Request;
using System.Collections.Generic;
using SysCommon.Repository.Domain;
using Infrastructure.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Infrastructure.Extensions;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 登录相关及用户资源分配（权限分配）
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly IAuth _authUtil;
        private ILogger _logger;
        private AuthStrategyContext _authStrategyContext;

        public CheckController(IAuth authUtil, ILogger<CheckController> logger)
        {
            _authUtil = authUtil;
            _logger = logger;
            _authStrategyContext = _authUtil.GetCurrentUser();
        }

        /// <summary>
        /// 根据token获取用户名称
        /// </summary>
        [HttpPost]
        public Response<string> GetUserName()
        {
            var result = new Response<string>();
            try
            {
                result.Result = _authStrategyContext.User.UserName;
            }
            catch (CommonException ex)
            {
                if (ex.Code == Define.INVALID_TOKEN)
                {
                    result.Code = ex.Code;
                    result.Message = ex.Message;
                }
                else
                {
                    result.Code = 500;
                    result.Message = ex.InnerException != null
                        ? ex.InnerException.Message : ex.Message;
                }

            }

            return result;
        }
        #region 获取登录用户资料
        /// <summary>
        /// 获取登录用户资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<SysUserView> GetUserProfile()
        {
            var resp = new Response<SysUserView>();
            try
            {
                resp.Result = _authStrategyContext.User.MapTo<SysUserView>();
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }

            return resp;
        }
        #endregion
        #region 检验token是否有效
        /// <summary>
        /// 检验token是否有效
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="requestid">备用参数.</param>
        [HttpPost]

        public Response<bool> GetStatus()
        {
            var result = new Response<bool>();
            try
            {
                result.Result = _authUtil.CheckLogin();
            }
            catch (Exception ex)
            {
                result.Code = Define.INVALID_TOKEN;
                result.Message = ex.Message;
            }

            return result;
        }
        #endregion
        #region 获取当前登录用户可访问的字段
        /// <summary>
        /// 获取当前登录用户可访问的字段
        /// </summary>
        /// <param name="tablename">实体名称（对应数据表名称）</param>
        /// <returns></returns>
        [HttpPost]
        public Response<List<KeyDescription>> GetProperties(string tablename)
        {
            var result = new Response<List<KeyDescription>>();
            try
            {
                result.Result = _authStrategyContext.GetProperties(tablename);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
        #endregion
        #region 获取登录用户的所有可访问的组织信息
        /// <summary>
        /// 获取登录用户的所有可访问的组织信息
        /// </summary>
        [HttpPost]
        public Response<List<SysOrg>> GetOrgs()
        {
            var result = new Response<List<SysOrg>>();
            try
            {
                result.Result = _authStrategyContext.Orgs;
            }
            catch (CommonException ex)
            {
                if (ex.Code == Define.INVALID_TOKEN)
                {
                    result.Code = ex.Code;
                    result.Message = ex.Message;
                }
                else
                {
                    result.Code = 500;
                    result.Message = ex.InnerException != null
                        ? "SysCommon.WebAPI数据库访问失败:" + ex.InnerException.Message
                        : "SysCommon.WebAPI数据库访问失败:" + ex.Message;
                }

            }

            return result;
        }
        #endregion
        #region  获取登录用户的所有可访问的角色
        /// <summary>
        /// 获取登录用户的所有可访问的角色
        /// </summary>
        [HttpPost]
        public Response<List<SysRole>> GetRoles()
        {
            var result = new Response<List<SysRole>>();
            try
            {
                result.Result = _authStrategyContext.Roles;
            }
            catch (CommonException ex)
            {
                if (ex.Code == Define.INVALID_TOKEN)
                {
                    result.Code = ex.Code;
                    result.Message = ex.Message;
                }
                else
                {
                    result.Code = 500;
                    result.Message = ex.InnerException != null
                        ? "SysCommon.WebAPI数据库访问失败:" + ex.InnerException.Message
                        : "SysCommon.WebAPI数据库访问失败:" + ex.Message;
                }

            }

            return result;
        }
        #endregion
        #region 获取登录用户的所有可访问的模块(栏目)及按钮，以列表形式返回结果
        /// <summary>
        /// 获取登录用户的所有可访问的模块(栏目)及按钮，以列表形式返回结果
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public Response<List<MenuView>> GetMenus()
        {
            var result = new Response<List<MenuView>>();
            try
            {
                result.Result = _authStrategyContext.Menus;
            }
            catch (CommonException ex)
            {
                if (ex.Code == Define.INVALID_TOKEN)
                {
                    result.Code = ex.Code;
                    result.Message = ex.Message;
                }
                else
                {
                    result.Code = 500;
                    result.Message = ex.InnerException != null
                        ? "SysCommon.WebAPI数据库访问失败:" + ex.InnerException.Message
                        : "SysCommon.WebAPI数据库访问失败:" + ex.Message;
                }

            }

            return result;
        }
        #endregion
        #region 获取登录用户的所有可访问的模块及菜单，以树状结构返回
        /// <summary>
        /// 获取登录用户的所有可访问的模块及菜单，以树状结构返回
        /// </summary>
        [HttpPost]
        public Response<IEnumerable<TreeItem<MenuView>>> GetModulesTree()
        {
            var result = new Response<IEnumerable<TreeItem<MenuView>>>();
            try
            {
                result.Result = _authStrategyContext.Menus.GenerateTree(u => u.Id, u => u.FatherId);
            }
            catch (CommonException ex)
            {
                if (ex.Code == Define.INVALID_TOKEN)
                {
                    result.Code = ex.Code;
                    result.Message = ex.Message;
                }
                else
                {
                    result.Code = 500;
                    result.Message = ex.InnerException != null
                        ? "OpenAuth.WebAPI数据库访问失败:" + ex.InnerException.Message
                        : "OpenAuth.WebAPI数据库访问失败:" + ex.Message;
                }

            }

            return result;
        }
        #endregion
        #region 登录接口
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="request">登录参数</param>
        /// <returns></returns>
        [HttpPost]
       //[CustomAuthorize(Roles ="0999")]
        [AllowAnonymous]
        public LoginResult Login([FromBody] PassportLoginRequest request)
        {
            _logger.LogInformation("Login enter");
            var result = new LoginResult();
            try
            {
                result = _authUtil.Login(request);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion
        #region 获取验证码
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public VierificationCodeResult GetVierificationCode()
        {
            var result = new VierificationCodeResult();
            try
            {
                string code = VierificationCode.RandomText();
                var data = new
                {
                    img = @"data:image/jpg;base64," + VierificationCode.CreateBase64Imgage(code),
                    uuid = Guid.NewGuid()
                };
               // HttpContext.GetService<IMemoryCache>().Set(data.uuid.ToString(), code, new TimeSpan(0, 5, 0));//5分钟 -- 设置验证码的有效时间（TimeSpan(int hours, int minutes, int seconds);）

                result.data = data;
            }
            catch (CommonException ex)
            {
                result.Code = 102;
                result.message = ex.Message;
                    
            }
            return result;
        }
        #endregion 
        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <param name="token"></param>
        /// <param name="requestid">备用参数.</param>
        [HttpPost]
        public Response<bool> Logout()
        {
            var resp = new Response<bool>();
            try
            {
                resp.Result = _authUtil.Logout();
            }
            catch (Exception e)
            {
                resp.Result = false;
                resp.Message = e.Message;
            }

            return resp;
        }
        #endregion
    }
}