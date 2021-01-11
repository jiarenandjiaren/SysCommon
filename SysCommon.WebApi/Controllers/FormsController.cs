using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SysCommon.App;
using SysCommon.App.Request;
using SysCommon.App.Response;
using SysCommon.Repository.Domain;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 表单操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly FormApp _app;

        [HttpGet]
        public Response<FormResp> Get(string id)
        {
            var result = new Response<FormResp>();
            try
            {
                result.Result = _app.FindSingle(id);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        //添加或修改
       [HttpPost]
        public Response Add(Form obj)
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

        //添加或修改
       [HttpPost]
        public Response Update(Form obj)
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

        /// <summary>
        /// 加载列表
        /// </summary>
        [HttpGet]
        public async Task<TableData> Load([FromQuery]QueryFormListReq request)
        {
            return await _app.Load(request);
        }

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

        public FormsController(FormApp app) 
        {
            _app = app;
        }
    }
}