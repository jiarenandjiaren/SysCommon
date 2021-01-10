/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 标签操作
* 类 名： LabelController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/12/02 13:10   N/A    初版
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
using SysCommon.Service.Response;
using System;
using Microsoft.Extensions.Logging;
using SysCommon.Service.Request;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 文章类型操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private ILogger _logger;
        public readonly LabelService _labelService;

        public LabelController(ILogger<LabelController> logger, LabelService labelService)
        {
            _logger = logger;
            _labelService = labelService;
        }
        #region 添加
        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public Response<ALabelReq> Add([FromBody] ALabelReq request)
        {
            var result = new Response<ALabelReq>();
            try
            {
                _labelService.Add(request);
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
        /// 修改标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public Response<ELabelReq> Update([FromBody] ELabelReq request)
        {
            var result = new Response<ELabelReq>();
            try
            {
                _labelService.Edit(request);
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
        /// 删除标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public Response Delete([FromBody] QueryOneReq request)
        {
            var result = new Response();
            try
            {
                _labelService.Delete(request);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }
        #endregion
        #region 获取标签列表 
        /// <summary>
        /// 获取标签列表 
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public TableData Show([FromBody] QueryLabelListReq request)
        {
            return _labelService.Show(request);
        }
        #endregion
        /// <summary>
        /// 展示单条数据
        /// </summary>
        /// <param name="updateAdminReq"></param>
        [AllowAnonymous]
        [HttpPost]
        public TableData GetDataOne([FromBody] QueryOneReq request)
        {
            return _labelService.GetDataOne(request);
        }
    }
}