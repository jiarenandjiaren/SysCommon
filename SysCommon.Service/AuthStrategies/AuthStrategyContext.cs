/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 授权策略上下文，一个典型的策略模式
*         根据用户账号的不同，采用不同的授权模式，以后可以扩展更多的授权方式 
*         根据用户账号的不同，采用不同的授权模式，以后可以扩展更多的授权方式
*         授权策略上下文，一个典型的策略模式
* 类 名： AuthStrategyContext
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/18 9:23   N/A    初版
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
    /// <summary>
    ///  授权策略上下文，一个典型的策略模式
    /// </summary>
    public class AuthStrategyContext
    {
        private readonly IAuthStrategy _strategy;
        public AuthStrategyContext(IAuthStrategy strategy)
        {
            _strategy = strategy;
        }

        public SysUser User
        {
            get { return _strategy.User; }
        }

        public List<MenuView> Menus
        {
            get { return _strategy.Menus; }
        }

        public List<Element> Elements
        {
            get { return _strategy.Elements; }
        }

        public List<SysRole> Roles
        {
            get { return _strategy.Roles; }
        }

        public List<Resource> Resources
        {
            get { return _strategy.Resources; }
        }

        public List<SysOrg> Orgs
        {
            get { return _strategy.Orgs; }
        }

        public List<KeyDescription> GetProperties(string tablename)
        {
            return _strategy.GetProperties(tablename);
        }

    }

}
