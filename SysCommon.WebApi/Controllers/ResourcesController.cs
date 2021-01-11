using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SysCommon.App;
using SysCommon.App.Interface;
using SysCommon.App.Request;
using SysCommon.App.Response;
using SysCommon.Repository.Domain;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 三方资源管理（暂时没有业务关联）
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ResourceApp _app;

        public ResourcesController(IAuth authUtil, ResourceApp app) 
        {
            _app = app;
        }
        [HttpGet]
        public async Task<TableData> Load([FromQuery]QueryResourcesReq request)
        {
            return await _app.Load(request);
        }

       [HttpPost]
        public Response Delete([FromBody]string[] ids)
        {
            Response resp = new Response();
            try
            {
                _app.Delete(ids);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return resp;
        }

       [HttpPost]
        public Response<string> Add(AddOrUpdateResReq obj)
        {
            var resp = new Response<string>();
            try
            {
                _app.Add(obj);
                resp.Result = obj.Id;
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return resp;
        }

       [HttpPost]
        public Response Update(AddOrUpdateResReq obj)
        {
            Response resp = new Response();
            try
            {
                _app.Update(obj);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return resp;
        }

        /// <summary>
        /// 加载角色资源
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="firstId">角色ID</param>
        [HttpGet]
        public Response<List<Resource>> LoadForRole(string appId, string firstId)
        {
            var result = new Response<List<Resource>>();
            try
            {
                result.Result = _app.LoadForRole(appId, firstId).ToList();

            }
            catch (Exception e)
            {
                result.Code = 500;
                result.Message = e.InnerException?.Message ?? e.Message;
            }

            return result;
        }

    }
}