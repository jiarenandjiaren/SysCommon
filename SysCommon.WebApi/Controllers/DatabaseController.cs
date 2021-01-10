/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 登录及与登录信息获取相关的接口（权限分配）
* 类 名： CheckController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 13:05   N/A    初版
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
    /// 数据库操作操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private ILogger _logger;
        public DatabaseController(DatabaseService databaseService, ILogger<DatabaseController> logger) 
        {
            _databaseService = databaseService;
            _logger = logger;
        }
        #region 获取数据库表
        /// <summary>
        /// 获取数据库表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public CheckData GetTableNames()
        {
            return _databaseService.GetTableNames();
        }
        #endregion
        #region 获取数据库一个表的所有属性值及属性描述
        /// <summary>
        /// 获取数据库一个表的所有属性值及属性描述
        /// </summary>
        /// <param name="moduleName">模块名称/表名</param>
        /// <returns></returns>
        [HttpPost]
        public CheckData GetProperties([FromBody] GetTableNameReq TableName)
        {
            return _databaseService.GetProperties(TableName);
        }
        #endregion
        #region 字段内容替换
        /// <summary>
        /// 字段内容替换
        /// </summary>
        /// <param name="contentReplaceReq"></param>
        [HttpPost]
        public WebResponseContent<string> ContentReplace(ContentReplaceReq contentReplaceReq)
        {
            var result = new WebResponseContent<string>();
            try
            {
                _databaseService.ContentReplace(contentReplaceReq);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
    }
}