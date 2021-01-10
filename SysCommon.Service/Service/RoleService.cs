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
    /// 角色服务
    /// </summary>
    public class RoleService : BaseService<SysRole>
    {
        public RoleService(IUnitWork unitWork, IRepository<SysRole> repository) : base(unitWork, repository)
        {
        }
        public void Add(RoleAddReq roleAddReq)
        {
            SysRole requser = roleAddReq;
            if (Repository.GetAll().Any(u => u.Name == roleAddReq.Name))
            {
                throw new ArgumentException("角色名："+ roleAddReq.Name + "已经存在" );
            }
            Repository.Add(requser);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="roleEditReq"></param>
        public void Edit(RoleEditReq roleEditReq)
        {
            SysRole requser = roleEditReq;
            if (Repository.GetAll().Any(u => u.Name == roleEditReq.Name && u.Id != roleEditReq.Id))
            {
                throw new ArgumentException("用户名：" + roleEditReq.Name + "已经存在");
            }
            UnitWork.Update<SysRole>(u => u.Id == requser.Id, u => new SysRole
            {
                Name = requser.Name,
                UpdateId = requser.UpdateId,
                UpdateTime = DateTime.Now,
                IsEnable = requser.IsEnable,
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
            UnitWork.Update<SysRole>(u => u.Id == request.Id, u => new SysRole
            {
                IsDelete = true
            });
        }
        /// <summary>
        /// 展示角色
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData Show(RoleQueryListReq roleQueryListReq)
        {


            var result = new TableData();
            

            var query = from role in UnitWork.Find<SysRole>(u => u.IsDelete == false)
                        join adminC in UnitWork.Find<SysUser>(null)
                        on role.CreateId equals adminC.Id into c
                        from ci in c.DefaultIfEmpty()
                        join adminU in UnitWork.Find<SysUser>(null)
                        on role.UpdateId equals adminU.Id into u
                        from ui in u.DefaultIfEmpty()
                        select new
                        {
                            role.Id,
                            role.Name,
                            CreateName = ci.UserName,
                            role.CreateTime,
                            UpdateName = ui.UserName,
                            role.UpdateTime,
                            role.IsDelete,
                            role.IsEnable,
                            role.Sort,
                            role.Description,
                        };
            if (roleQueryListReq.state == 0)
                query = query.Where(u => u.IsEnable == false);
            else if (roleQueryListReq.state == 1)
                query = query.Where(u => u.IsEnable == true);

            result.data = query.OrderByDescending(u => u.Sort)
                .Skip((roleQueryListReq.page - 1) * roleQueryListReq.limit)
                .Take(roleQueryListReq.limit);
            result.count = query.Count();
            return result;
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
                var query = (from role in UnitWork.Find<SysRole>(u => u.IsDelete == false && u.Id == request.Id)//
                             select new SysRole
                             {
                                 Id = role.Id,
                                 Name = role.Name,
                                 CreateTime = role.CreateTime,
                                 UpdateTime = role.UpdateTime,
                                 IsEnable = role.IsEnable,
                                 Sort = role.Sort,
                                 Description = role.Description,

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
        /// <summary>
        /// 加载特定文章的属性
        /// </summary>
        /// <param name="userId"></param>
        public List<Repository.Domain.SysRole> LoadForAdmin(string userId)
        {
            var result = from art in UnitWork.Find<AdminRoleTop>(null)
                         join r in UnitWork.Find<Repository.Domain.SysRole>(null) on
                         art.RoleId equals r.Id
                         where art.AdminId == userId
                         select r;
            return result.ToList();
        }
        /// <summary>
        /// 获取指定用户角色id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<string> RoleIds(QueryOneReq request)
        {
            var _userRoleIds = UnitWork.Find<Relevance>(u => u.TopFirstId == request.Id && u.TopKey == Define.USERROLE).Select(u => u.TopFirstId).ToList();//获取用户对应的角色Id
            return  (List<string>)UnitWork.Find<SysRole>(u => _userRoleIds.Contains(u.Id)).Where(u => u.IsDelete == false).Select(u=>u.Id);
        }
    }
}