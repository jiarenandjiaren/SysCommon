/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 组织操作
* 类 名： SysOrgController
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 13:14   N/A    初版
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
    /// 组织管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class SysOrgController : ControllerBase
    {
        private readonly SysOrgService  _sysOrgService ;
        private ILogger _logger;
        public SysOrgController(SysOrgService sysOrgService,ILogger<SysOrgController> logger) 
        {
            _sysOrgService = sysOrgService;
            _logger = logger;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="aSysOrgReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<ASysOrgReq> Add([FromBody] ASysOrgReq  aSysOrgReq)
        {
            var result = new WebResponseContent<ASysOrgReq>();
            try
            {
                _sysOrgService.Add(aSysOrgReq);
                result.Data = aSysOrgReq;
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
        /// <param name="eSysOrgReq"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<ESysOrgReq> Edit([FromBody] ESysOrgReq eSysOrgReq)
        {
            var result = new WebResponseContent<ESysOrgReq>();
            try
            {
                _sysOrgService.Edit(eSysOrgReq);
                result.Data = eSysOrgReq;
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
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent<string> Delete([FromBody]string[] Id)
        {
            var result = new WebResponseContent<string>();
            try
            {
                _sysOrgService.Delete(Id);
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
        /// </summary>
        /// <param name="sysOrgQueryList"></param>
        /// <returns></returns>
        [HttpPost]
        public TableData Show([FromBody] SysOrgQueryListReq  sysOrgQueryList)
        {
            return _sysOrgService.Show(sysOrgQueryList);
        }
    }
}