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
    /// <summary>
    /// 栏目操作
    /// </summary>
    public class MenuService : BaseService<Menu>
    {
        private readonly IAuth _authUtil;
        
        private readonly RelevanceService _relevanceService;
        public MenuService(IUnitWork unitWork, IRepository<Menu> repository, RelevanceService relevanceService, IAuth auth) : base(unitWork, repository)
        {
            _relevanceService = relevanceService;
            _authUtil = auth;
        }
        public void Add(AMenuReq aMenuReq)
        {
            Menu requser = aMenuReq;
            if (Repository.GetAll().Any(u => u.MenuName == aMenuReq.MenuName))
            {
                throw new ArgumentException("栏目名：" + aMenuReq.MenuName + "已经存在");
            }
            Repository.Add(requser);
        }
        public void Update(EMenuReq eMenuReq)
        {
            Menu requser = eMenuReq;
            if (Repository.GetAll().Any(u => u.MenuName == eMenuReq.MenuName && u.Id != eMenuReq.Id))
            {
                throw new ArgumentException("栏目名：" + eMenuReq.MenuName + "已经存在");
            }
            UnitWork.Update<Menu>(u => u.Id == requser.Id, u => new Menu
            {
                FatherId = requser.FatherId,
                MenuName = requser.MenuName,
                URL = requser.URL,
                TemplateURL = requser.TemplateURL,
                BackstageMenuUrl = requser.BackstageMenuUrl,
                FrontMenuUrl = requser.FrontMenuUrl,
                FrontArticleUrl = requser.FrontArticleUrl,
                IsEnable = requser.IsEnable,
                UpdateTime = DateTime.Now,
                UpdateId = requser.UpdateId,
                Sort = requser.Sort,
                Description = requser.Description
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public void Delete(QueryOneReq request)
        {
            UnitWork.Update<Menu>(u => u.Id == request.Id, u => new Menu
            {
                IsDelete = true
            });
        }
        /// <summary>
        /// 展示
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData Show(MenuQueryListReq menuQueryListReq)
        {
            var query = from menu in UnitWork.Find<Menu>(u => u.IsDelete == false)
                        join  adminC in UnitWork.Find<SysUser>(null)
                        on menu.CreateId equals adminC.Id into c
                        from ci in c.DefaultIfEmpty()
                        join adminU in UnitWork.Find<SysUser>(null)
                        on menu.UpdateId equals adminU.Id into u
                        from ui in u.DefaultIfEmpty()
                        select new
                        {
                            menu.Id,
                            menu.FatherId,
                            menu.MenuName,
                            menu.URL,
                            menu.TemplateURL,
                            CreateName = ci.UserName,
                            menu.CreateTime,
                            UpdateName = ui.UserName,
                            menu.UpdateTime,
                            menu.BackstageMenuUrl,
                            menu.FrontMenuUrl,
                            menu.FrontArticleUrl,
                            menu.IsDelete,
                            menu.IsEnable,
                            menu.Sort,
                            menu.Description,
                        };
            if (menuQueryListReq.state == 0)
                query = query.Where(u => u.IsEnable == false);
            else if (menuQueryListReq.state == 1)
                query = query.Where(u => u.IsEnable == true);
            var query1 = query
                .OrderBy(u => u.Sort)
                .Skip((menuQueryListReq.page - 1) * menuQueryListReq.limit)
                .Take(menuQueryListReq.limit);

            var records = query.Count();
            return new TableData
            {
                count = records,
                data = query1,
            };
        }
        /// <summary>
        /// 展示单条数据
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData GetDataOne(QueryOneReq request)
        {
            TableData tableData = new TableData();
            try
            {
                var query = (from menu in UnitWork.Find<Menu>(u => u.IsDelete == false && u.Id == request.Id)//文章表               [Description("父级栏
                             select new MenuView
                             {
                                 Id = menu.Id,
                                 FatherId = menu.FatherId,
                                 MenuName = menu.MenuName,
                                 URL = menu.URL,
                                 IconName = menu.IconName,
                                 CreateTime = menu.CreateTime,
                                 UpdateTime = menu.UpdateTime,
                                 IsEnable = menu.IsEnable,
                                 Sort = menu.Sort,
                                 Description = menu.Description,

                             }).ToList();
                tableData.data = query;
            }
            catch (Exception ex)
            {
                tableData.Code = 102;
                tableData.message = ex.InnerException?.Message ?? ex.Message;
            }
            return tableData;
        }
    }
}