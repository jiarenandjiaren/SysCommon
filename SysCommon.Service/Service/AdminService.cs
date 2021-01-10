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
    public class AdminService : BaseService<SysUser>
    {
       // private readonly AdminRoleTopService _adminRoleTopService;
        private readonly RoleService _roleService;
        private readonly CreateEditNameService _createEditNameService;
        public AdminService(IUnitWork unitWork, IRepository<SysUser> repository,  RoleService roleService, CreateEditNameService  createEditNameService ) : base(unitWork, repository)
        {
            _createEditNameService = createEditNameService;
               _roleService = roleService;
        }
        public void Add(ASysUserReq aSysUserReq)
        {
            SysUser requser = aSysUserReq;
            if (Repository.GetAll().Any(u => u.UserName == aSysUserReq.UserName))
            {
                throw new ArgumentException("用户名：" + aSysUserReq.UserName + "已经存在");
            }
            Repository.Add(requser);
            //2020.10.21 19.57  (添加用户时取消关联的角色id，在权限分配画面单独操作)
            //_adminRoleTopService.Add(aSysUserReq, requser.Id);
        }
        public void Edit(ESysUserReq  eSysUserReq)
        {
            SysUser requser = eSysUserReq;
            if (Repository.GetAll().Any(u => u.UserName == eSysUserReq.UserName && u.Id != eSysUserReq.Id))
            {
                throw new ArgumentException("用户名：" + eSysUserReq.UserName + "已经存在");
            }
            UnitWork.Update<SysUser>(u => u.Id == requser.Id, u => new SysUser
            {
                UserName = requser.UserName,
                Password = requser.Password,
                UpdateId = requser.UpdateId,
                Phone = requser.Phone,
                Email = requser.Email,
                UpdateTime = DateTime.Now,
                IsEnable = requser.IsEnable,
                IsDelete = requser.IsDelete,
                Sort = requser.Sort,
                Description = requser.Description
            });
            //_adminRoleTopService.Edit(eSysUserReq, requser.Id);
        }
        /// <summary>
        /// 删除（假删）
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public void Delete(QueryOneReq request)
        {
            UnitWork.Update<SysUser>(u => u.Id == request.Id, u => new SysUser
            {
                IsDelete = true
            });
        }
        /// <summary>
        /// 展示
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData Show(QuerySysUserListReq querySysUserListReq)
        {
            IQueryable<SysUser> query = UnitWork.Find<SysUser>(null).Where(u => u.IsDelete == false);
            if (!string.IsNullOrEmpty(querySysUserListReq.UserName))
                query = query.Where(u => u.UserName == querySysUserListReq.UserName);
            if (querySysUserListReq.state == 0)
                query = query.Where(u => u.IsEnable == false);
            else if (querySysUserListReq.state == 1)
                query = query.Where(u => u.IsEnable == true);
            var query1 = query
                .OrderBy(u => u.Sort)
                .Skip((querySysUserListReq.page - 1) * querySysUserListReq.limit)
                .Take(querySysUserListReq.limit);

            var records = query.Count();
            var article = new List<SysUserView>();
            foreach (var art in query1.ToList())
            {
                SysUserView av = art;
                av.Id = art.Id;
                av.UserName = art.UserName;
                av.Password = art.Password;
                av.Phone = art.Phone;
                av.Email = art.Email;
                av.CreateTime = art.CreateTime;
                av.UserName = art.UserName;
                av.UpdateTime = art.UpdateTime;
                av.LoginCount = art.LoginCount;
                av.LastLoginTime = art.LastLoginTime;
                av.IsDelete = art.IsDelete;
                av.IsEnable = art.IsEnable;
                av.Sort = art.Sort;
                av.Description = av.Description;
                var attribute = _roleService.LoadForAdmin(art.Id);
                var cname = _createEditNameService.LoadForAdmin(art.CreateId);
                var uname = _createEditNameService.LoadForAdmin(art.UpdateId);
                av.RoleName = string.Join(",", attribute.Select(u => u.Name).ToList());//多个参数通过“，”隔开 
                av.RoleId = string.Join(",", attribute.Select(u => u.Id).ToList());
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
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="listId"></param>
        public void IsEnable(string[] listId)
        {
            foreach (var i in listId)
            {
                UnitWork.Update<SysUser>(u => u.Id == i, u => new SysUser
                {
                    IsEnable = false
                });
            }
        }
        /// <summary>
        /// 记录登录次数
        /// </summary>
        /// <param name="eAdminReq"></param>
        public void Count(int logincount,string username)
        {
            UnitWork.Update<SysUser>(u => u.UserName == username, u => new SysUser
            {
                LoginCount = logincount,
                LastLoginTime = DateTime.Now,
            });
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
                var query = UnitWork.Find<SysUser>(u => u.IsDelete == false && u.Id == request.Id);
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