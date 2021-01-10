/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 普通用户授权策略
* 类 名： NormalAuthStrategy
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/17 16:00   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/

using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.Service
{
    /// <summary>
    /// 普通用户授权策略
    /// </summary>
    public class NormalAuthStrategy : BaseService<SysUser>, IAuthStrategy
    {
        //用户角色
        public NormalAuthStrategy(IUnitWork unitWork, IRepository<SysUser> repository, DbExtension dbExtension) : base(unitWork, repository, null)
        {
            _dbExtension = dbExtension;
        }
        protected SysUser _user;

        private List<string> _userRoleIds;    //用户角色GUID
        private DbExtension _dbExtension;

        public List<MenuView> Menus
        {
            get
            {
                var menuids = UnitWork.Find<Relevance>(
                    u =>
                        (u.TopKey == Define.ROLEMENU && _userRoleIds.Contains(u.TopFirstId))).Select(u => u.TopSecondId);

                var menus = (from menu in UnitWork.Find<Menu>(u => menuids.Contains(u.Id))
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
                                 IsDelete = menu.IsDelete,
                                 Sort = menu.Sort
                             }).Where(u => u.IsDelete == false).OrderByDescending(u=>u.Sort).ToList();

                var usermoduleelements = Elements;

                foreach (var menu in menus)
                {
                    menu.Elements = usermoduleelements.Where(u => u.MenuId == menu.Id).ToList();
                }

                return menus ;
            }
        }

        public List<Element> Elements
        {
            get
            {
                var elementIds = UnitWork.Find<Relevance>(
                    u =>
                        (u.TopKey == Define.ROLEMENU && _userRoleIds.Contains(u.TopFirstId))).Select(u => u.TopSecondId);
                var userelements = UnitWork.Find<Element>(u => elementIds.Contains(u.MenuId));
                return userelements.ToList();
            }
        }

        public List<SysRole> Roles
        {
            get { return UnitWork.Find<SysRole>(u => _userRoleIds.Contains(u.Id)).Where(u => u.IsDelete == false).OrderByDescending(u=>u.Sort).ToList(); }
        }

        public List<Resource> Resources
        {
            get
            {
                var resourceIds = UnitWork.Find<Relevance>(
                    u =>
                        (u.TopKey == Define.ROLERESOURCE && _userRoleIds.Contains(u.TopFirstId))).Select(u => u.TopFirstId);
                return UnitWork.Find<Resource>(u => resourceIds.Contains(u.Id)).ToList();
            }
        }

        public List<SysOrg> Orgs
        {
            get
            {
                var orgids = UnitWork.Find<Relevance>(
                    u => u.TopFirstId == _user.Id && u.TopKey == Define.USERORG).Select(u => u.TopSecondId);
                return UnitWork.Find<SysOrg>(u => orgids.Contains(u.Id)).ToList();
            }
        }

        public SysUser User
        {
            get { return _user; }
            set
            {
                _user = value;
                _userRoleIds = UnitWork.Find<Relevance>(u => u.TopFirstId == _user.Id && u.TopKey == Define.USERROLE).Select(u => u.TopFirstId).ToList();//获取用户对应的角色Id
            }
        }

        /// <summary>
        /// 获取用户可访问的字段列表
        /// </summary>
        /// <param name="moduleCode">模块的code</param>
        /// <returns></returns>
        public List<KeyDescription> GetProperties(string tablename)
        {
            //            //和部分逻辑冲突，需要屏蔽掉
            //            var module = UnitWork.FindSingle<Module>(u =>u.Code == moduleCode);
            //             if(module == null)
            //            {
            //                throw new Exception("该模块不存在");
            //            }
            //            if(module.IsSys){
            //                throw new Exception("系统内置模块，不能进行字段分配");
            //            }

            var allprops = _dbExtension.GetProperties(tablename);
            var props = UnitWork.Find<Relevance>(u =>
                     u.TopKey == Define.ROLEDATAPROPERTY && _userRoleIds.Contains(u.TopFirstId) && u.TopSecondId == tablename)
                .Select(u => u.TopThirdId);

            return allprops.Where(u => props.Contains(u.Key)).ToList();
        }

        
    }
}