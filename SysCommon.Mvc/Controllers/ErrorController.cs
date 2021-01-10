// ***********************************************************************
// Assembly         : SysCommon.Mvc
// Author           : 李玉宝
// Created          : 07-24-2018
//
// Last Modified By : 李玉宝
// Last Modified On : 07-24-2018
// ***********************************************************************
// <copyright file="ErrorController.cs" company="SysCommon.Mvc">
//     Copyright (c) http://www.SysCommon.me. All rights reserved.
// </copyright>
// <summary>
// 异常处理页面
//</summary>
// ***********************************************************************

using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SysCommon.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public string Demo()
        {
            return JsonHelper.Instance.Serialize(new Response
            {
                Code = 500,
                Message = "演示版本，不要乱动"
            });
        }

        [AllowAnonymous]
        public ActionResult Auth()
        {
            return View();
        }
    }
}