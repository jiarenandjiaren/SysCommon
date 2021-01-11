using System;
using System.Collections.Generic;
using SysCommon.App.Interface;
using SysCommon.App.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;
using System.Linq;
using SysCommon.App.Request;
using SysCommon.Repository;

namespace SysCommon.App
{
    public class RoleApp : BaseApp<Role,SysCommonDBContext>
    {
        private RevelanceManagerApp _revelanceApp;

        /// <summary>
        /// 加载当前登录用户可访问的全部角色
        /// </summary>
        public List<Role> Load(QueryRoleListReq request)
        {
            var loginUser = _auth.GetCurrentUser();
            var roles = loginUser.Roles;
            if (!string.IsNullOrEmpty(request.key))
            {
                roles = roles.Where(u => u.Name.Contains(request.key)).ToList();
            }

            return roles;
        }


        /// <summary>
        /// 添加角色，如果当前登录用户不是System，则直接把新角色分配给当前登录用户
        /// </summary>
        public void Add(RoleView obj)
        {
           UnitWork.ExecuteWithTransaction(() =>
           {
               Role role = obj;
               role.CreateTime = DateTime.Now;
               UnitWork.Add(role);
               UnitWork.Save();
               obj.Id = role.Id;   //要把保存后的ID存入view
           
               //如果当前账号不是SYSTEM，则直接分配
               var loginUser = _auth.GetCurrentUser();
               if (loginUser.User.Account != Define.SYSTEM_USERNAME)
               {
                   _revelanceApp.Assign(new AssignReq
                   {
                       type = Define.USERROLE,
                       firstId = loginUser.User.Id,
                       secIds = new[] {role.Id}
                   });
               }
           });
        }
        
        /// <summary>
        /// 更新角色属性
        /// </summary>
        /// <param name="obj"></param>
        public void Update(RoleView obj)
        {
            Role role = obj;

            UnitWork.Update<Role>(u => u.Id == obj.Id, u => new Role
            {
                Name = role.Name,
                Status = role.Status
            });

        }


        public RoleApp(IUnitWork<SysCommonDBContext> unitWork, IRepository<Role,SysCommonDBContext> repository,
            RevelanceManagerApp app,IAuth auth) : base(unitWork, repository, auth)
        {
            _revelanceApp = app;
        }
    }
}