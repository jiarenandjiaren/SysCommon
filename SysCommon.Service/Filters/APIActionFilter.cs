
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SysCommon.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SysCommon.Service.Filters
{

    public class APIActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                Infrastructure.Response result = new Infrastructure.Response();
                result.Message = string.Empty;
                result.Code = (int)ResponseType.ParameterVerify;
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors )
                    {
                        result.Message += error.ErrorMessage + "，"+"\n";
                        //result.Data = "{}";
                    }
                }
                result.Message = result.Message.TrimEnd(',');
                context.Result = new JsonResult(result);
            }
            //else
            //{
            //    var result = context.Result as ObjectResult ?? null;
            //    context.Result = new OkObjectResult(new Response
            //    {
            //        Message = "成功",
            //        Data = result == null ? "{}" : result.Value
            //    });
            //    base.OnResultExecuting(context);

            //}
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
