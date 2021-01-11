using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SysCommon.App;
using SysCommon.App.Interface;
using SysCommon.App.Request;
using SysCommon.App.Response;

namespace SysCommon.Mvc.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly CategoryApp _app;
        private CategoryTypeApp _categoryTypeApp;
        public CategoriesController(IAuth authUtil, CategoryApp app, CategoryTypeApp categoryTypeApp) : base(authUtil)
        {
            _app = app;
            _categoryTypeApp = categoryTypeApp;
        }

        //
        // GET: /UserManager/
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> All([FromQuery]QueryCategoryListReq request)
        {
            TableData data = new TableData();
            data = await _app.Load(request);
            return JsonHelper.Instance.Serialize(data);
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
        public string Add(AddOrUpdateCategoryReq obj)
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
        public string Update(AddOrUpdateCategoryReq obj)
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

        /// <summary>
        /// 得到所有的字典定义
        /// </summary>
        /// <returns></returns>
        public string AllTypes()
        {
            var data = _categoryTypeApp.AllTypes();
            return JsonHelper.Instance.Serialize(data);
        }

    }
}