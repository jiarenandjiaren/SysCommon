/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 关联数据的配置
* 类 名： RelevanceController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 14:50   N/A    初版
*
* Copyright (c) 2020 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SysCommon.Service;
using SysCommon.Service.Request;
using SysCommon.Service.Response;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 关联数据的配置
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class RelevanceController : ControllerBase
    {
        private readonly RelevanceService _relevanceService;
        private ILogger _logger;
        public RelevanceController(RelevanceService relevanceService,ILogger<RelevanceController> logger) 
        {
            _relevanceService = relevanceService;
            _logger = logger;
        }
        /// <summary>
        /// 为角色分配数据字段权限
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<ARelevanceRoleToPropertyReq> AddRoleToProperties([FromBody] ARelevanceRoleToPropertyReq request)
        {
            var result = new Response<ARelevanceRoleToPropertyReq>();
            try
            {
                _relevanceService.AddRoleToProperties(request);
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #region  为角色分配用户，需要统一提交，会删除以前该角色的所有用户
        /// <summary>
        /// 为角色分配用户，需要统一提交，会删除以前该角色的所有用户
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<ARoleUsers> AddRoleUsers([FromBody] ARoleUsers request)
        {
            var result = new Response<ARoleUsers>();
            try
            {
                _relevanceService.AddRoleUsers(request);
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 为用户分配角色，需要统一提交，会删除以前该用户的所有角色
        /// <summary>
        /// 为用户分配角色，需要统一提交，会删除以前该用户的所有角色
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<AUserRoles> AddUserRoles([FromBody] AUserRoles request)
        {
            var result = new Response<AUserRoles>();
            try
            {
                _relevanceService.AddUserRoles(request);
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 为部门分配用户，需要统一提交，会删除以前该部门的所有用户
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<AOrgUsers> AddOrgUsers([FromBody] AOrgUsers request)
        {
            var result = new Response<AOrgUsers>();
            try
            {
                _relevanceService.AddOrgUsers(request);
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 为角色分配模块（栏目），需要统一提交，会删除以前该角色的所有模块（栏目）
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<ARoleMenus> AddRoleMenus([FromBody] ARoleMenus request)
        {
            var result = new Response<ARoleMenus>();
            try
            {
                _relevanceService.AddRoleMenus(request);
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 为角色分配按钮，需要统一提交，会删除以前该角色的所有按钮
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<ARoleElement> AddRoleElement([FromBody] ARoleElement request)
        {
            var result = new Response<ARoleElement>();
            try
            {
                _relevanceService.AddRoleElement(request);
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
    }
}