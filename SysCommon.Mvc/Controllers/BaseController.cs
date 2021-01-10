// ***********************************************************************
// Assembly         : SysCommon.Mvc
// Author           : yubaolee
// Created          : 07-11-2016
//
// Last Modified By : yubaolee
// Last Modified On : 07-19-2016
// Contact : www.cnblogs.com/yubaolee
// File: BaseController.cs
// ***********************************************************************

using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SysCommon.Service.Interface;

namespace SysCommon.Mvc.Controllers
{
    /// <summary>
    /// 基础控制器
    /// <para>新增于2016-07-19 11:12:09</para>
    /// </summary>
    public class BaseController : Controller
    {
        protected WebResponseContent Result = new WebResponseContent();
        protected IAuth _authUtil;

        public BaseController(IAuth authUtil)
        {
            _authUtil = authUtil;
        }

    }
}