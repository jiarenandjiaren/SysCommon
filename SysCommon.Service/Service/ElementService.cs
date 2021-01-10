/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 权限按钮操作
* 类 名： ElementServic
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 8:06   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
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

namespace SysCommon.Service
{
    public class ElementServic : BaseService<Element>
    {
        private readonly CreateEditNameService _createEditNameService;
        public ElementServic(IUnitWork unitWork, IRepository<Element> repository, CreateEditNameService  createEditNameService ) : base(unitWork, repository)
        {
            _createEditNameService = createEditNameService;
        }
        public void Add(AElementReq aElementReq)
        {
            Element requser = aElementReq;
            if (Repository.GetAll().Any(u => u.DomId == aElementReq.DomId))
            {
                throw new ArgumentException("权限：" + aElementReq.DomId + "已经存在，不可重复添加！");
            }
            Repository.Add(requser);
        }
        public void Edit(EElementReq eElementReq)
        {
            Element requser = eElementReq;
            if (Repository.GetAll().Any(u => u.DomId == eElementReq.DomId && u.MenuId != eElementReq.MenuId))
            {
                //同意模块不能有重复的按钮
                throw new ArgumentException("权限：" + eElementReq.DomId + "已经存在，不可重复添加");
            }
            UnitWork.Update<Element>(u => u.Id == requser.Id, u => new Element
            { 
                DomId = requser.DomId,
                Name = requser.Name,
                Script = requser.Script,
                Icon = requser.Icon,
                Class = requser.Class,
                UpdateTime = DateTime.Now,
                UpdateId = requser.UpdateId,
                IsEnable = requser.IsEnable,
                IsDelete = requser.IsDelete,
                Sort = requser.Sort,
                Description = requser.Description
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public void Delete(string[] listId)
        {
            foreach (var i in listId)
            {
                UnitWork.Update<Element>(u => u.Id == i, u => new Element
                {
                    IsDelete = true
                });
            }
        }
        /// <summary>
        /// 展示按钮
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData Show(QueryElementListReq queryElementListReq)
        {
            IQueryable<Element> query = UnitWork.Find<Element>(null).Where(u => u.IsDelete == false );
            if (!string.IsNullOrEmpty(queryElementListReq.MenuId))//指定模块所有的按钮
                query.Where(u=>u.MenuId == queryElementListReq.MenuId);
            if (queryElementListReq.state == 0)
                query = query.Where(u => u.IsEnable == false);
            else if (queryElementListReq.state == 1)
                query = query.Where(u => u.IsEnable == true);
            var query1 = query
                .OrderBy(u => u.Sort)
                .Skip((queryElementListReq.page - 1) * queryElementListReq.limit)
                .Take(queryElementListReq.limit);

            var records = query.Count();
            var element = new List<ElementView>();
            foreach (var art in query1.ToList())
            {
                ElementView av = art;
                av.Id = art.Id;
                av.DomId = art.DomId;
                av.Name = art.Name;
                av.Script = art.Script;
                av.CreateTime = art.CreateTime;
                av.Icon = art.Icon;
                av.UpdateTime = art.UpdateTime;
                av.Class = art.Class;
                av.IsDelete = art.IsDelete;
                av.IsEnable = art.IsEnable;
                av.Sort = art.Sort;
                av.Description = av.Description;
                var cname = _createEditNameService.LoadForAdmin(art.CreateId);
                var uname = _createEditNameService.LoadForAdmin(art.UpdateId);
                av.CreateName = string.Join(",", cname.Select(u => u.UserName).ToList());
                av.UpdateName = string.Join(",", uname.Select(u => u.UserName).ToList());
                element.Add(av);
            }
            return new TableData
            {
                count = records,
                data = element,
            };
        }
    }
}