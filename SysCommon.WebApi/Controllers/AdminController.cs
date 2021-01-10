/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 管理员操作
* 类 名： AdminController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:49   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
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
    /// 管理员操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        private ILogger _logger;
        public AdminController(AdminService adminService,ILogger<AdminController> logger) 
        {
            _adminService = adminService;
            _logger = logger;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<ASysUserReq> Add([FromBody] ASysUserReq aSysUserReq)
        {
            var result = new WebResponseContent<ASysUserReq>();
            try
            {
                _adminService.Add(aSysUserReq);
                //result.Result = addAdminReq;
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<ESysUserReq> Update([FromBody] ESysUserReq eSysUserReq)
        {
            var result = new WebResponseContent<ESysUserReq>();
            try
            {
                _adminService.Edit(eSysUserReq);
                //result.Result = updateAdminReq;
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<string> Delete([FromBody] QueryOneReq  queryOneReq)
        {
            var result = new WebResponseContent<string>();
            try
            {
                _adminService.Delete(queryOneReq);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 加载列表
        /// 获取管理员列表
        /// </summary>
       // [AllowAnonymous]
        [HttpPost]
        public TableData Show([FromBody] QuerySysUserListReq querySysUserListReq)
        {
            return _adminService.Show(querySysUserListReq);
        }
        
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<string> IsEnable([FromBody] string[] Id)
        {
            var result = new WebResponseContent<string>();
            try
            {
                _adminService.IsEnable(Id);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #region 展示单条数据
        /// <summary>
        /// 展示单条数据
        /// </summary>
        /// <param name="updateAdminReq"></param>
        [HttpPost]
        public TableData GetDataOne([FromBody] QueryOneReq request)
        {
            return _adminService.GetDataOne(request);
        }
        #endregion
    }
}