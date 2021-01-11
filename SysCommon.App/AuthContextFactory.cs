﻿// ***********************************************************************
// Assembly         : SysCommon.App
// Author           : 李玉宝
// Created          : 07-05-2018
//
// Last Modified By : 李玉宝
// Last Modified On : 07-05-2018
// ***********************************************************************
// <copyright file="AuthContextFactory.cs" company="SysCommon.App">
//     Copyright (c) http://www.SysCommon.me. All rights reserved.
// </copyright>
// <summary>
// 用户权限策略工厂
//</summary>
// ***********************************************************************

using SysCommon.Repository;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.App
{
    /// <summary>
    ///  加载用户所有可访问的资源/机构/模块
    /// <para>李玉宝新增于2016-07-19 10:53:30</para>
    /// </summary>
    public class AuthContextFactory
    {
        private SystemAuthStrategy _systemAuth;
        private NormalAuthStrategy _normalAuthStrategy;
        private readonly IUnitWork<SysCommonDBContext> _unitWork;

        public AuthContextFactory(SystemAuthStrategy sysStrategy
            , NormalAuthStrategy normalAuthStrategy
            , IUnitWork<SysCommonDBContext> unitWork)
        {
            _systemAuth = sysStrategy;
            _normalAuthStrategy = normalAuthStrategy;
            _unitWork = unitWork;
        }

        public AuthStrategyContext GetAuthStrategyContext(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;

            IAuthStrategy service = null;
             if (username == Define.SYSTEM_USERNAME)
            {
                service= _systemAuth;
            }
            else
            {
                service = _normalAuthStrategy;
                service.User = _unitWork.FirstOrDefault<User>(u => u.Account == username);
            }

         return new AuthStrategyContext(service);
        }
    }
}