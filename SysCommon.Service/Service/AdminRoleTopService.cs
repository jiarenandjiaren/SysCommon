using System;
using System.Collections.Generic;
using SysCommon.Service.Interface;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;
using System.Linq;
using SysCommon.Service.Request;

namespace SysCommon.Service
{
    //public class AdminRoleTopService : BaseService<AdminRoleTop>
    //{
    //    //public AdminRoleTopService(IUnitWork unitWork, IRepository<AdminRoleTop> repository) : base(unitWork, repository)
    //    //{
    //    //}
    //    //public void Add(ASysUserReq aAdminReq,string AdminId)
    //    //{
    //    //    List<EAdminRoleReq> adminRoleTops = aAdminReq.adminRoleReqs;
    //    //    AdminRoleTop requser = adminRoleTops[0];
    //    //    //Repository.Delete(u => u.AdminId == aAdminReq.Id);//删除指定角色所有数据
    //    //    foreach (var i in adminRoleTops)
    //    //    {
    //    //        requser = i;
    //    //        requser.AdminId = AdminId;
    //    //        Repository.Add(requser);
    //    //    }
    //    //}
    //    //public void Edit(ESysUserReq eAdminReq, string AdminId)
    //    //{
    //    //    List<EAdminRoleReq> adminRoleTops = eAdminReq.RoleIds;
    //    //    AdminRoleTop requser = adminRoleTops[0];
    //    //    Repository.Delete(u => u.AdminId == eAdminReq.Id);//删除指定角色所有数据
    //    //    foreach (var i in adminRoleTops)
    //    //    {
    //    //        requser = i;
    //    //        requser.AdminId = AdminId;
    //    //        Repository.Add(requser);
    //    //    }
    //    //}
    //}
}