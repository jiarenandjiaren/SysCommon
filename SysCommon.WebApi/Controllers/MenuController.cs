/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 栏目管理
* 类 名： MenuController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 13:12   N/A    初版
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
using SysCommon.Service;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using Microsoft.Extensions.Logging;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 栏目管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class MenuController : ControllerBase
    {
        private ILogger _logger;
        private readonly MenuService _menuService;
        public MenuController(MenuService menuService,ILogger<MenuController> logger) 
        {
            _menuService = menuService;
            _logger = logger;
        }
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<AMenuReq> Add([FromBody]AMenuReq menuAddOrEditReq)
        {
            var result = new WebResponseContent<AMenuReq>();
            try
            {
                _menuService.Add(menuAddOrEditReq);
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
        public WebResponseContent<EMenuReq> Update([FromBody] EMenuReq menuAddOrEditReq)
        {
            var result = new WebResponseContent<EMenuReq>();
            try
            {
                _menuService.Update(menuAddOrEditReq);
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
                _menuService.Delete(request);
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
        /// 获取栏目
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public TableData Show([FromBody] MenuQueryListReq menuQueryListReq)
        {
            return _menuService.Show(menuQueryListReq);
        }
        #endregion 
        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="menuQueryListReq"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public TableData GetDataOne([FromBody] QueryOneReq request)
        {
            return _menuService.GetDataOne(request);
        }
        #endregion 
    }
}