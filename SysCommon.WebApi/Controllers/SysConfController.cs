using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SysCommon.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysCommon.WebApi.Controllers
{
    /// <summary>
    /// 系统配置信息（停用）
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SysConfController : ControllerBase
    {
        private IOptions<AppSetting> _appConfiguration;

        public SysConfController(IOptions<AppSetting> appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        /// <summary>
        /// 是否Identity认证
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public Response<bool> IsIdentityAuth()
        {
            return new Response<bool>
            {
                Result = _appConfiguration.Value.IsIdentityAuth
            };
            //var result = new Response<bool>();
            //try
            //{
            //    result.Data = _appConfiguration.Value.IsIdentityAuth;
            //}
            //catch (Exception ex)
            //{
            //    result.Code = Define.INVALID_TOKEN;
            //    result.Message = ex.Message;
            //}
            //return result;
        }
    }
}
