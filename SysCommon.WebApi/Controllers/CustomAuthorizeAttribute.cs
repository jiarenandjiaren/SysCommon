using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysCommon.WebApi.Controllers
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public string Roles { get; set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Microsoft.AspNetCore.Http.HttpRequest request = context.HttpContext.Request;
            //string token = request.Headers["token"].ToString();

            //拿到用户信息，匹配角色是否可以访问
            string defailtrole = "999111";
            if (Roles.Contains(defailtrole) == false)
            {
                throw new Exception("暂无访问权限");
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
