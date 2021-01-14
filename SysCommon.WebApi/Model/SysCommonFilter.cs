using System.Reflection;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SysCommon.App;
using SysCommon.App.Interface;
using SysCommon.Repository.Domain;

namespace SysCommon.WebApi.Model
{
    //public class SysCommonFilter : IActionFilter
    //{
    //    private readonly IAuth _authUtil;
    //    private readonly SysLogApp _logApp;

    //    public SysCommonFilter(IAuth authUtil, SysLogApp logApp)
    //    {
    //        _authUtil = authUtil;
    //        _logApp = logApp;
    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        var description =
    //            (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;

    //        var Controllername = description.ControllerName.ToLower();
    //        var Actionname = description.ActionName.ToLower();

    //        //匿名标识
    //        var authorize = description.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute));
    //        if (authorize != null)
    //        {
    //            return;
    //        }

    //        if (!_authUtil.CheckLogin())
    //        {
    //            context.HttpContext.Response.StatusCode = 401;
    //            context.Result = new JsonResult(new Response
    //            {
    //                Code = 401,
    //                Message = "认证失败，请提供认证信息"
    //            });
    //            return;
    //        }
    //        _logApp.Add(new SysLog
    //        {
    //            Content = $"用户访问",
    //            Href = $"{Controllername}/{Actionname}",
    //            CreateName = _authUtil.GetUserName(),
    //            CreateId = _authUtil.GetCurrentUser().User.Id,
    //            TypeName = "访问日志"
    //        });
    //    }

    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        return;
    //    }
    //}








    public class SysCommonFilter : IActionFilter
    {
        private readonly IAuth _authUtil;
        private readonly SysLogApp _logApp;

        public SysCommonFilter(IAuth authUtil, SysLogApp logApp)
        {
            _authUtil = authUtil;
            _logApp = logApp;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var description =
                (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;

            // 请求的控制器名称
            var Controllername = description.ControllerName.ToLower();
            // 请求的方法名称
            var Actionname = description.ActionName.ToLower();

            //匿名标识
            var authorize = description.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute));
            if (authorize != null)
            {
                return;
            }

            if (!_authUtil.CheckLogin())
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult(new Response
                {
                    Code = 101,
                    Message = "认证失败！"
                });
            }
            else
            {
                _logApp.Add(new SysLog
                {
                    Content = $"用户访问",
                    Href = $"{Controllername}/{Actionname}",
                    CreateName = _authUtil.GetUserName(),
                    CreateId = _authUtil.GetCurrentUser().User.Id,
                    TypeName = "访问日志"
                });
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
    }
}
