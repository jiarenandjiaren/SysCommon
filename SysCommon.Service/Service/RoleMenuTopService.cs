using System;
using System.Collections.Generic;
using SysCommon.Service.Interface;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;
using System.Linq;
using SysCommon.Service.Request;
using Infrastructure;

namespace SysCommon.Service
{
    public class RoleMenuTopService : BaseService<RoleMenuTop>
    {
        public RoleMenuTopService(IUnitWork unitWork, IRepository<RoleMenuTop> repository) : base(unitWork, repository)
        {
        }
        /// <summary>
        /// 为角色分配栏目
        /// </summary>
        /// <param name="roleMenuAddOrEditReq"></param>
        public void MenuToRole(ARoleMenuReq roleMenuAddOrEditReq)
        {
            RoleMenuTop requser = roleMenuAddOrEditReq;
            Repository.Delete(u => u.RoleId == roleMenuAddOrEditReq.RoleId);//删除指定角色所有数据
            foreach (var i in roleMenuAddOrEditReq.MenuId)
            {
                requser.MenuId = i;
                Repository.Add(requser);
            }
        }
        /// <summary>
        /// 展示用户拥有角色拥有的所有栏目
        /// </summary>
        /// <param name="adminid"></param>
        public CheckData LoadAdminOfMenu(string adminid)
        {
            var listrole = from adminroletop in UnitWork.Find<AdminRoleTop>(u=>u.AdminId == adminid)
                           select new
                           { 
                               adminroletop.RoleId 
                           };
            string roleids =  string.Join(",", listrole.Select(u => u.RoleId).ToList());//用户对应权限
            var ListMenu = from rolemenu in UnitWork.Find<RoleMenuTop>(u => roleids.Contains(u.RoleId)) .DefaultIfEmpty()
                             join menu in UnitWork.Find<Menu>(u => u.IsDelete == false && u.IsEnable == true)
                             on rolemenu.MenuId equals menu.Id 
                             select new
                             {
                                 menu.Id,
                                 menu.MenuName,
                                 menu.FatherId,
                                 menu.Sort,
                                 menu.BackstageMenuUrl
                             };
            return new CheckData
            {
                data = ListMenu,
            };
        }
    }
}