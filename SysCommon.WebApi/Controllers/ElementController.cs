/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 权限按钮操作
* 类 名： ElementController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 8:06   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Threading.Tasks;
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
    /// 为模块分配按钮
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class ElementController : ControllerBase
    {
        private readonly ElementServic _elementServic;
        private ILogger _logger;
        public ElementController(ElementServic elementServic,ILogger<AdminController> logger) 
        {
            _elementServic = elementServic;
            _logger = logger;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="addAdminReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<AElementReq> Add([FromBody] AElementReq aElementReq)
        {
            var result = new WebResponseContent<AElementReq>();
            try
            {
                _elementServic.Add(aElementReq);
                result.Data = aElementReq;
            }
            catch (Exception ex)
            {
                result.Code = 101;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return  result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="eElementReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<EElementReq> Edit([FromBody] EElementReq eElementReq)
        {
            var result = new WebResponseContent<EElementReq>();
            try
            {
                _elementServic.Edit(eElementReq);
                result.Data = eElementReq;
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
        public WebResponseContent<string> Delete([FromBody]string[] Id)
        {
            var result = new WebResponseContent<string>();
            try
            {
                _elementServic.Delete(Id);
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
        public TableData Show([FromBody] QueryElementListReq queryElementListReq)
        {
            return _elementServic.Show(queryElementListReq);
        }
    }
}