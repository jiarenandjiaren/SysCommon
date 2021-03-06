﻿using System;
using System.Linq;

namespace SysCommon.App.SSO
{
    public class AppInfoService 
    {
        public AppInfo Get(string appKey)
        {
            //可以从数据库读取
            return _applist.SingleOrDefault(u => u.AppKey == appKey);
        }

        private AppInfo[] _applist = new[]
        {
            new AppInfo
            {
                AppKey = "SysCommon",
                Icon = "/Areas/SSO/Content/images/logo.png",
                IsEnable = true,
                Remark = "基于DDDLite的权限管理系统",
                ReturnUrl = "http://localhost:56813",
                Title = "SysCommon.Core",
                CreateTime = DateTime.Now,
            },
            new AppInfo
            {
                AppKey = "SysCommontest",
                Icon = "/Areas/SSO/Content/images/logo.png",
                IsEnable = true,
                Remark = "这只是个模拟的测试站点",
                ReturnUrl = "http://localhost:53050",
                Title = "SysCommon.Core测试站点",
                CreateTime = DateTime.Now,
            }
        };
    }
}