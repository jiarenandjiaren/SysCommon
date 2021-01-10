/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 授权策略接口
* 类 名： IAuthStrategy
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/18 9:25   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System.Collections.Generic;
using Infrastructure;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;

namespace SysCommon.Service
{
    public interface IAuthStrategy 
    {
         List<MenuView> Menus { get; }

        List<Element> Elements { get; }

        List<SysRole> Roles { get; }

        List<Resource> Resources { get; }

        List<SysOrg> Orgs { get; }

         SysUser User
        {
            get;set;
        }

        /// <summary>
        /// 根据模块id获取可访问的模块字段
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        List<KeyDescription> GetProperties(string tablename);

    }
}