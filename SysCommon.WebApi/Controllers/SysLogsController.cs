/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 系统日志（停用）
* 类 名： SysLogsController
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
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SysCommon.Service;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 系统日志（停用）
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SysLogsController : ControllerBase
    {
        private readonly SysLogApp _app;
        public SysLogsController(SysLogApp app)
        {
            _app = app;
        }
        #region 获取详情
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Response<SysLog> Get(string id)
        {
            var result = new Response<SysLog>();
            try
            {
                result.Result = _app.Get(id);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
        #endregion
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        public Response Add(SysLog obj)
        {
            var result = new Response();
            try
            {
                _app.Add(obj);

            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
        #endregion
        #region 修改日志（建议废弃）
        /// <summary>
        /// 修改日志（建议废弃）
        /// </summary>
        [HttpPost]
        public Response Update(SysLog obj)
        {
            var result = new Response();
            try
            {
                _app.Update(obj);

            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
        #endregion
        #region 加载列表
        /// <summary>
        /// 加载列表
        /// </summary>
        [HttpGet]
        public TableData Load([FromQuery]QuerySysLogListReq request)
        {
            return _app.Load(request);
        }
        #endregion
        #region 批量删除
        /// <summary>
        /// 批量删除
        /// </summary>
        [HttpPost]
        public Response Delete([FromBody]string[] ids)
        {
            var result = new Response();
            try
            {
                _app.Delete(ids);

            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
        #endregion 
    }
}
