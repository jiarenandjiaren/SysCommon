/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 组织服务
* 类 名： Sys_OrgService
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 11:31   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using SysCommon.Service.Interface;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;
using System.Linq;
using SysCommon.Service.Request;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;

namespace SysCommon.Service
{
    public class SysOrgService : BaseService<SysOrg>
    {
        private readonly CreateEditNameService _createEditNameService;
        public SysOrgService(IUnitWork unitWork, IRepository<SysOrg> repository, CreateEditNameService  createEditNameService ) : base(unitWork, repository)
        {
            _createEditNameService = createEditNameService;
        }
        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="aOrgReq"></param>
        public void Add(ASysOrgReq aOrgReq)
        {
            SysOrg requser = aOrgReq;
            if (Repository.GetAll().Any(u => u.Name == aOrgReq.Name))
            {
                throw new ArgumentException("组织名：" + aOrgReq.Name + "已经存在");
            }
            Repository.Add(requser);
        }
        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="eSysOrgReq"></param>
        public void Edit(ESysOrgReq eSysOrgReq)
        {
            SysOrg requser = eSysOrgReq;
            if (Repository.GetAll().Any(u => u.Name == eSysOrgReq.Name && u.Id != eSysOrgReq.Id))
            {
                throw new ArgumentException("组织名：" + eSysOrgReq.Name + "已经存在");
            }
            UnitWork.Update<SysOrg>(u => u.Id == requser.Id, u => new SysOrg
            {
                FatherId = requser.FatherId,
                Name = requser.Name,
                IconName = requser.IconName,
                UpdateId = requser.UpdateId,
                UpdateTime = DateTime.Now,
                IsEnable = requser.IsEnable,
                IsDelete = requser.IsDelete,
                Sort = requser.Sort,
                Description = requser.Description
            });
        }
        /// <summary>
        /// 删除指定组织信息
        /// </summary>
        /// <param name="listId"></param>
        public void Delete(string[] listId)
        {
            foreach (var i in listId)
            {
                UnitWork.Update<SysOrg>(u => u.Id == i, u => new SysOrg
                {
                    IsDelete = true
                });
            }
        }
        /// <summary>
        /// 展示组织信息
        /// </summary>
        /// <param name="sysOrgQueryListReq"></param>
        /// <returns></returns>
        public TableData Show(SysOrgQueryListReq sysOrgQueryListReq)
        {
            IQueryable<SysOrg> query = UnitWork.Find<SysOrg>(null).Where(u => u.IsDelete == false);
            if (sysOrgQueryListReq.state == 0)
                query = query.Where(u => u.IsEnable == false);
            else if (sysOrgQueryListReq.state == 1)
                query = query.Where(u => u.IsEnable == true);
            var query1 = query
                .OrderBy(u => u.Sort)
                .Skip((sysOrgQueryListReq.page - 1) * sysOrgQueryListReq.limit)
                .Take(sysOrgQueryListReq.limit);

            var records = query.Count();
            var article = new List<SysOrgView>();
            foreach (var art in query1.ToList())
            {
                SysOrgView av = art;
                av.Id = art.Id;
                av.FatherId = art.FatherId;
                av.Name = art.Name;
                av.IconName = art.IconName;
                av.CreateTime = art.CreateTime;
                av.UpdateTime = art.UpdateTime;
                av.IsDelete = art.IsDelete;
                av.IsEnable = art.IsEnable;
                av.Sort = art.Sort;
                av.Description = av.Description;
                var cname = _createEditNameService.LoadForAdmin(art.CreateId);
                var uname = _createEditNameService.LoadForAdmin(art.UpdateId);
                av.CreateName = string.Join(",", cname.Select(u => u.UserName).ToList());
                av.UpdateName = string.Join(",", uname.Select(u => u.UserName).ToList());
                article.Add(av);
            }
            return new TableData
            {
                count = records,
                data = article,
            };
        }
    }
}