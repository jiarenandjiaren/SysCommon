﻿using System;
using Infrastructure;
using Infrastructure.Cache;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SysCommon.Service.Request;
using SysCommon.Service.SSO;

namespace SysCommon.Service.Test
{
    [TestFixture]
    public class TestForm :TestBase
    {
        public override ServiceCollection GetService()
        {
            var services = new ServiceCollection();

            var cachemock = new Mock<ICacheContext>();
            cachemock.Setup(x => x.Get<UserAuthSession>("tokentest")).Returns(new UserAuthSession { Name = "admin" });
            services.AddScoped(x => cachemock.Object);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext.Request.Query[Define.TOKEN_NAME]).Returns("tokentest");

            services.AddScoped(x => httpContextAccessorMock.Object);

            return services;
        }


        //[Test]
        //public void Load()
        //{
        //    var app = _autofacServiceProvider.GetService<FormApp>();
        //    var result = app.Load(new QueryFormListReq
        //    {
        //        page = 1,
        //        limit = 10
        //    });
        //    Console.WriteLine(JsonHelper.Instance.Serialize(result));
        //}
    }
}
