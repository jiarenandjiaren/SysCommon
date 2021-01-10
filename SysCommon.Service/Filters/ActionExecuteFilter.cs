using Microsoft.AspNetCore.Mvc.Filters;
using SysCommon.Service.ObjectActionValidator.ExpressValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysCommon.Service.Filters
{
    public class ActionExecuteFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //验证方法参数
            //context.ActionParamsValidator();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
