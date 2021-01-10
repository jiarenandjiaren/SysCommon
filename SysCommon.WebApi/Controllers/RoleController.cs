/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 角色管理
* 类 名： RoleController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 13:14   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SysCommon.Service;
using SysCommon.Service.Request;
using SysCommon.Service.Response;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
   
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        private ILogger _logger;
        public RoleController(RoleService  roleService, ILogger<RoleController> logger) 
        {
            _logger = logger;
            _roleService = roleService;
        }
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<RoleAddReq> Add([FromBody] RoleAddReq  roleAddReq)
        {
            var result = new WebResponseContent<RoleAddReq>();
            try
            {
                _roleService.Add(roleAddReq);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<RoleEditReq> Update([FromBody] RoleEditReq roleEditReq)
        {
            var result = new WebResponseContent<RoleEditReq>();
            try
            {
                _roleService.Edit(roleEditReq);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<string> Delete([FromBody] QueryOneReq request)
        {
            var result = new WebResponseContent<string>();
            try
            {
                _roleService.Delete(request);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 加载列表
        /// <summary>
        /// 加载列表
        /// </summary>
        /// <param name="queryAdminListReq"></param>
        /// <returns></returns>
        [HttpPost]
        public TableData Show([FromBody] RoleQueryListReq  roleQueryListReq)
        {
            return _roleService.Show(roleQueryListReq);
        }
        #endregion  
        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="menuQueryListReq"></param>
        /// <returns></returns>
        [HttpPost]
        public TableData GetDataOne([FromBody] QueryOneReq request)
        {
            return _roleService.GetDataOne(request);
        }
        #endregion
        /// <summary>
        /// 获取指定用户角色id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<List<string>> RoleIds([FromBody] QueryOneReq request)
        {
            var result = new WebResponseContent<List<string>>();
            try
            {
                result.Data = _roleService.RoleIds(request);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
    }
}