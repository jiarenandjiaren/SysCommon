/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 关联数据服务
* 类 名： RelevanceService
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 15:11   N/A    初版
*
* Copyright (c) 2020 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;
using System.Linq;
using SysCommon.Service.Request;
using SysCommon.Service.Interface;

namespace SysCommon.Service
{
    /// <summary>
    /// 关联数据服务
    /// </summary>
    public class RelevanceService : BaseService<Relevance>
    {
        public RelevanceService(IUnitWork unitWork, IRepository<Relevance> repository) : base(unitWork, repository)
        {
        }
        /// <summary>
        /// 为角色分配用户，需要统一提交，会删除以前该角色的所有用户
        /// </summary>
        /// <param name="request"></param>
        public void AddRoleUsers(ARoleUsers request)
        {
            //删除以前的所有用户
            UnitWork.Delete<Relevance>(u => u.TopSecondId == request.RoleId && u.TopKey == Define.USERROLE);
            //批量分配用户角色
            UnitWork.BatchAdd((from firstId in request.UserIds
                               select new Relevance
                               {
                                   TopKey = Define.USERROLE,
                                   TopFirstId = firstId,
                                   TopSecondId = request.RoleId,
                                   CreateTime = DateTime.Now,
                                   CreateId = request.CreateId
                               }).ToArray());
            UnitWork.Save();
        }
        /// <summary>
        /// 为用户分配角色，需要统一提交，会删除以前该用户的所有角色
        /// </summary>
        /// <param name="request"></param>
        public void AddUserRoles(AUserRoles request)
        {
            //删除以前当前用户的所有角色
            UnitWork.Delete<Relevance>(u => u.TopFirstId == request.UserId && u.TopKey == Define.USERROLE);
            //批量分配用户角色
            UnitWork.BatchAdd((from secondids in request.RoleIds
                               select new Relevance
                               {
                                   TopKey = Define.USERROLE,
                                   TopFirstId = request.UserId,
                                   TopSecondId = secondids,
                                   CreateTime = DateTime.Now,
                                   CreateId = request.CreateId
                               }).ToArray());
            UnitWork.Save();
        }
        /// <summary>
        /// 为角色分配数据字段权限
        /// </summary>
        /// <param name="request"></param>
        public void AddRoleToProperties(ARelevanceRoleToPropertyReq request)
        { 
            //重置之前的数据
            UnitWork.Delete<Relevance>(u => u.TopFirstId == request.RoleId && u.TopKey == Define.ROLEDATAPROPERTY);
            var relevances = new List<Relevance>();
            foreach (var requestProperty in request.Properties)
            {
                relevances.Add(new Relevance
                {
                    TopKey = Define.ROLEDATAPROPERTY,
                    TopFirstId = request.RoleId,
                    TopSecondId = request.TableName,
                    TopThirdId = requestProperty,
                    CreateTime = DateTime.Now,
                    CreateId = request.CreateId
                });
            }
            UnitWork.BatchAdd(relevances.ToArray());
            UnitWork.Save();
        }
        /// <summary>
        /// 为部门分配用户，需要统一提交，会删除以前该部门的所有用户
        /// </summary>
        /// <param name="request"></param>
        public void AddOrgUsers(AOrgUsers request)
        {
            //删除以前的所有用户
            UnitWork.Delete<Relevance>(u => u.TopSecondId == request.OrgId && u.TopKey == Define.USERORG);
            //批量分配用户角色
            UnitWork.BatchAdd((from firstId in request.UserIds
                               select new Relevance
                               {
                                   TopKey = Define.USERORG,
                                   TopFirstId = firstId,
                                   TopSecondId = request.OrgId,
                                   CreateTime = DateTime.Now,
                                   CreateId = request.CreateId
                               }).ToArray());
            UnitWork.Save();
        }
        /// <summary>
        /// 为模块分配角色，需要统一提交，会删除以前该角色的所有模块
        /// </summary>
        /// <param name="request"></param>
        public void AddRoleMenus(ARoleMenus request)
        {
            //删除以前的所有栏目
            UnitWork.Delete<Relevance>(u => u.TopSecondId == request.RoleId && u.TopKey == Define.ROLEMENU);
            //批量分配用户栏目
            UnitWork.BatchAdd((from firstId in request.MenuIds
                               select new Relevance
                               {
                                   TopKey = Define.ROLEMENU,
                                   TopFirstId = firstId,
                                   TopSecondId = request.RoleId,
                                   CreateTime = DateTime.Now,
                                   CreateId = request.CreateId
                               }).ToArray());
            UnitWork.Save();
        }
        /// <summary>
        /// 为角色分配按钮，需要统一提交，会删除以前该角色的所有按钮
        /// </summary>
        /// <param name="request"></param>
        public void AddRoleElement(ARoleElement request)
        {
            //删除以前的所有按钮
            UnitWork.Delete<Relevance>(u => u.TopSecondId == request.RoleId && u.TopKey == Define.ROLEELEMENT);
            //批量分配角色按钮
            UnitWork.BatchAdd((from firstId in request.ElementIds
                               select new Relevance
                               {
                                   TopKey = Define.ROLEELEMENT,
                                   TopFirstId = firstId,
                                   TopSecondId = request.RoleId,
                                   CreateTime = DateTime.Now,
                                   CreateId = request.CreateId
                               }).ToArray());
            UnitWork.Save();
        }
    }
}