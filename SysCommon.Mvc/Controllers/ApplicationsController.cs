using System;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SysCommon.Service;
using SysCommon.Service.Interface;
using SysCommon.Service.Request;
using SysCommon.Repository.Domain;

namespace SysCommon.Mvc.Controllers
{
    public class ApplicationsController : BaseController
    {
        private readonly AppManager _app;


        public string GetList([FromQuery]QueryAppListReq request)
        {
            var resp = new Response<List<Application>>();
            try
            {
                resp.Result = _app.GetList(request);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

       [HttpPost]
        public string Delete(string[] ids)
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
            return JsonHelper.Instance.Serialize(resp);
        }

       [HttpPost]
        public string Add(Application obj)
        {
            Response resp = new Response();
            try
            {
                _app.Add(obj);
            }
            catch (Exception e)
            {
                resp.Code = 500;
                resp.Message = e.Message;
            }
            return JsonHelper.Instance.Serialize(resp);
        }

       [HttpPost]
        public string Update(Application obj)
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
            return JsonHelper.Instance.Serialize(resp);
        }


        public ApplicationsController(IAuth authUtil, AppManager app) : base(authUtil)
        {
            _app = app;
        }
    }
}