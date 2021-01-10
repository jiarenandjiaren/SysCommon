/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 配置HttpContext
* 类 名： StaticHttpContextExtensions
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/25 10:01   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
    /// <summary>
    ///  配置HttpContext
    /// </summary>
    public static class StaticHttpContextExtensions
    {
        //public static void AddHttpContextAccessor(this IServiceCollection services)
        //{
        //    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //}

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            Utilities.HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }
}
