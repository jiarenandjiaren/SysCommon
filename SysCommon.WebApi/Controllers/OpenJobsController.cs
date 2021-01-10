using System;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SysCommon.Service;
using SysCommon.Service.Request;
using SysCommon.Service.Response;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 定时任务操作
    /// 【系统模块】
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OpenJobsController : ControllerBase
    {
        private readonly OpenJobService _openJobService;
        public OpenJobsController(OpenJobService openJobService)
        {
            _openJobService = openJobService;
        }
        ////获取详情
        //[HttpGet]
        //public Response<OpenJob> Get(string id)
        //{
        //    var result = new Response<OpenJob>();
        //    try
        //    {
        //        result.Result = _openJobService.Get(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Code = 500;
        //        result.Message = ex.InnerException?.Message ?? ex.Message;
        //    }

        //    return result;
        //}

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent Add(AddOrUpdateOpenJobReq obj)
        {
            var result = new WebResponseContent();
            try
            {
                _openJobService.Add(obj);
            }
            catch (Exception ex)
            {
                result.Code = 102;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public WebResponseContent Edit(AddOrUpdateOpenJobReq obj)
        {
            var result = new WebResponseContent();
            try
            {
                _openJobService.Update(obj);
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
        [HttpPost]
        public TableData Show([FromQuery] QueryOpenJobListReq request)
        {
            return _openJobService.Load(request);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        [HttpPost]
        public WebResponseContent Delete([FromBody] string[] ids)
        {
            var result = new WebResponseContent();
            try
            {
                _openJobService.Delete(ids);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取本地可执行的任务列表
        /// </summary>
        [HttpPost]
        public WebResponseContent<List<string>> QueryLocalHandlers()
        {
            var result = new WebResponseContent<List<string>>();
            try
            {
                result.Data = _openJobService.QueryLocalHandlers();
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 改变任务状态，启动/停止
        /// </summary>
        [HttpPost]
        public WebResponseContent ChangeStatus(ChangeJobStatusReq req)
        {
            var result = new WebResponseContent();
            try
            {
                _openJobService.ChangeJobStatus(req);
                result.Message = "切换成功，可以在系统日志中查看运行结果";
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
