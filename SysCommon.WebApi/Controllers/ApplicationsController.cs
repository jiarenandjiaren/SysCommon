using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SysCommon.App;
using SysCommon.App.Request;
using SysCommon.App.Response;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 应用管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppManager _app;

        public ApplicationsController(AppManager app) 
        {
            _app = app;
        }
        /// <summary>
        /// 加载应用列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<TableData> Load([FromQuery]QueryAppListReq request)
        {
            var applications =await _app.GetList(request);
            return new TableData
            {
                data = applications,
                count = applications.Count
            };
        }

    }
}