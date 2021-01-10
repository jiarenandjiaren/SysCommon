/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 领域服务
* 类 名： SystemAuthStrategy
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

using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.Service
{
    /// <summary>
    /// 领域服务
    /// <para>超级管理员权限</para>
    /// <para>超级管理员使用guid.empty为ID，可以根据需要修改</para>
    /// </summary>
    public class SystemAuthStrategy : BaseService<SysUser>, IAuthStrategy
    {
        protected SysUser _user;
        private DbExtension _dbExtension;
        public SystemAuthStrategy(IUnitWork unitWork, IRepository<SysUser> repository, DbExtension dbExtension) : base(unitWork, repository, null)
        {
            _dbExtension = dbExtension;
            _user = new SysUser
            {
                UserName = Define.SYSTEM_USERNAME,
                // UserName = "超级管理员",
                Id = Guid.Empty.ToString()
            };
        }
        public List<MenuView> Menus
        {
            get
             {
                var menus = (from menu in UnitWork.Find<Menu>(null)
                               select new MenuView
                               {
                                   FatherId = menu.FatherId,
                                   MenuName = menu.MenuName,
                                   URL = menu.URL,
                                   Id = menu.Id,
                                   TemplateURL = menu.TemplateURL,
                                   BackstageMenuUrl = menu.BackstageMenuUrl,
                                   FrontMenuUrl = menu.FrontMenuUrl,
                                   FrontArticleUrl = menu.FrontArticleUrl,
                                   IconName = menu.IconName,
                                   Sort = menu.Sort,//!=null? menu.Sort : 0,
                                   UpdateTime = menu.UpdateTime,//!=null?menu.UpdateTime:null,
                                   Description = menu.Description, //!= null ? menu.Description : null,
                                   IsDelete = menu.IsDelete,
                               }).Where(u => u.IsDelete == false).OrderByDescending(u => u.Sort).ToList();

                foreach (var menu in menus)
                {
                    menu.Elements = UnitWork.Find<Element>(u => u.MenuId == menu.Id).ToList();
                }

                return menus;
            }
        }

        public List<SysRole> Roles
        {
            get { return UnitWork.Find<SysRole>(u => u.IsDelete == false).OrderByDescending(u => u.Sort).ToList(); }
        }

        public List<Element> Elements
        {
            get { return UnitWork.Find<Element>(null).ToList(); }
        }

        public List<Resource> Resources
        {
            get { return UnitWork.Find<Resource>(null).ToList(); }
        }

        public List<SysOrg> Orgs
        {
            get { return UnitWork.Find<SysOrg>(null).ToList(); }
        }

        public SysUser User
        {
            get { return _user; }
            set   //禁止外部设置
            {
                throw new Exception("超级管理员，禁止设置用户");
            }
        }

        public List<KeyDescription> GetProperties(string moduleCode)
        {
            //            var module = UnitWork.FindSingle<Module>(u =>u.Code == moduleCode);
            //            if(module == null)
            //            {
            //                throw new Exception("该模块不存在");
            //            }
            //            if(module.IsSys){
            //                throw new Exception("系统内置模块，不能进行字段分配");
            //            }
            return _dbExtension.GetProperties(moduleCode);
        }
    }
}