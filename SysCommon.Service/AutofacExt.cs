// ***********************************************************************
// Assembly         : SysCommon.Mvc
// Author           : yubaolee
// Created          : 10-26-2015
//
// Last Modified By : yubaolee
// Last Modified On : 10-26-2015
// ***********************************************************************
// <copyright file="AutofacExt.cs" company="www.cnblogs.com/yubaolee">
//     Copyright (c) www.cnblogs.com/yubaolee. All rights reserved.
// </copyright>
// <summary>IOC扩展</summary>
// ***********************************************************************

using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.Quartz;
using Infrastructure.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SysCommon.Service.Interface;
using SysCommon.Service.SSO;
using SysCommon.Repository;
using SysCommon.Repository.Interface;
using IContainer = Autofac.IContainer;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ConfigurationSection = Microsoft.Extensions.Configuration.ConfigurationSection;

namespace SysCommon.Service
{
    public static  class AutofacExt
    {
        public static IConfiguration Configuration { get; }
        //private static IContainer _container;
        //public static IContainer InitForTest(IServiceCollection services)
        //{
        //    var builder = new ContainerBuilder();

        //    //注册数据库基础操作和工作单元
        //    services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        //    services.AddScoped(typeof(IUnitWork), typeof(UnitWork));

        //    //如果当前项目是webapi，则必须是本地,否则会引起死循环
        //    if (Assembly.GetEntryAssembly().FullName.Contains("SysCommon.WebApi"))
        //    {
        //        services.AddScoped(typeof(IAuth), typeof(LocalAuth));
        //    }
        //    else  //如果是MVC或者单元测试，则可以根据情况调整，默认是本地授权，无需SysCommon.WebApi、Identity
        //    {
        //        services.AddScoped(typeof(IAuth), typeof(LocalAuth));
        //        //如果想使用WebApi SSO授权，请使用下面这种方式
        //        //services.AddScoped(typeof(IAuth), typeof(ApiAuth));
        //    }

        //    //注册app层
        //    builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

        //    //防止单元测试时已经注入
        //    if (services.All(u => u.ServiceType != typeof(ICacheContext)))
        //    {
        //        services.AddScoped(typeof(ICacheContext), typeof(CacheContext));
        //    }

        //    if (services.All(u => u.ServiceType != typeof(IHttpContextAccessor)))
        //    {
        //        services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
        //    }

        //    builder.RegisterModule(new QuartzAutofacFactoryModule());

        //    builder.Populate(services);

        //    _container = builder.Build();
        //    return _container;

        //}


        public static void InitAutofac(ContainerBuilder builder)
        {
            //注册数据库基础操作和工作单元(仓储)
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>));
            builder.RegisterType(typeof(UnitWork)).As(typeof(IUnitWork));
          
            //如果当前项目是webapi，则必须是本地,否则会引起死循环
            if (Assembly.GetEntryAssembly().FullName.Contains("SysCommon.WebApi"))
            {
                builder.RegisterType(typeof(LocalAuth)).As(typeof(IAuth));           
            }
            else  //如果是MVC或者单元测试，则可以根据情况调整，默认是本地授权，无需SysCommon.WebApi、Identity
            {
                builder.RegisterType(typeof(LocalAuth)).As(typeof(IAuth));
                //如果想使用WebApi SSO授权，请使用下面这种方式
                //builder.RegisterType(typeof(ApiAuth)).As(typeof(IAuth));
            }
            //注册app层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            //缓存
            builder.RegisterType(typeof(CacheContext)).As(typeof(ICacheContext));
            //访问器
            builder.RegisterType(typeof(HttpContextAccessor)).As(typeof(IHttpContextAccessor));
            //定时任务
            builder.RegisterModule(new QuartzAutofacFactoryModule());
        }
    }
}