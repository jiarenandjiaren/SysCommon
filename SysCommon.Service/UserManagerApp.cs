using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using SysCommon.Service.Interface;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;


namespace SysCommon.Service
{
    public class UserManagerApp : BaseService<SysUser>
    {
        private RevelanceManagerApp _revelanceApp;
        private OrgManagerApp _orgManagerApp;

        public SysUser GetByAccount(string account)
        {
            return Repository.FindSingle(u => u.UserName == account);
        }

        /// <summary>
        /// 加载当前登录用户可访问的一个部门及子部门全部用户
        /// </summary>
        public TableData Load(QueryUserListReq request)
        {
            var loginUser = _auth.GetCurrentUser();

            string cascadeId = ".0.";
            //if (!string.IsNullOrEmpty(request.orgId))
            //{
            //    var org = loginUser.Orgs.SingleOrDefault(u => u.Id == request.orgId);
            //    cascadeId = org.CascadeId;
            //}

            IQueryable<User> query = UnitWork.Find<User>(null);
            if (!string.IsNullOrEmpty(request.key))
            {
                query = UnitWork.Find<User>(u => u.Name.Contains(request.key) || u.Account.Contains(request.key));
            }

            var ids = loginUser.Orgs.Where(u => u.CreateId.Contains(cascadeId)).Select(u => u.Id).ToArray();
            var userIds = _revelanceApp.Get(Define.USERORG, false, ids);

            var users = query.Where(u => userIds.Contains(u.Id))
                .OrderBy(u => u.Name)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit);

            var records = query.Count(u => userIds.Contains(u.Id));


            var userviews = new List<UserView>();
            foreach (var user in users.ToList())
            {
                UserView uv = user;
                var orgs = _orgManagerApp.LoadForUser(user.Id);
                uv.Organizations = string.Join(",", orgs.Select(u => u.Name).ToList());
                uv.OrganizationIds = string.Join(",", orgs.Select(u => u.Id).ToList());
                userviews.Add(uv);
            }

            return new TableData
            {
                count = records,
                data = userviews,
            };
        }

        public void AddOrUpdate(UpdateUserReq request)
        {
            if (string.IsNullOrEmpty(request.OrganizationIds))
                throw new Exception("请为用户分配机构");
            User requser = request;
            requser.CreateId = _auth.GetCurrentUser().User.Id;
            if (string.IsNullOrEmpty(request.Id))
            {
                if (UnitWork.IsExist<User>(u => u.Account == request.Account))
                {
                    throw new Exception("用户账号已存在");
                }

                if (string.IsNullOrEmpty(requser.Password))
                {
                    requser.Password = requser.Account;   //如果客户端没提供密码，默认密码同账号
                }

                requser.CreateTime = DateTime.Now;

                UnitWork.Add(requser);
                request.Id = requser.Id; //要把保存后的ID存入view
            }
            else
            {
                UnitWork.Update<User>(u => u.Id == request.Id, u => new User
                {
                    Account = requser.Account,
                    BizCode = requser.BizCode,
                    Name = requser.Name,
                    Sex = requser.Sex,
                    Status = requser.Status
                });
                if (!string.IsNullOrEmpty(requser.Password))  //密码为空的时候，不做修改
                {
                    UnitWork.Update<User>(u => u.Id == request.Id, u => new User
                    {
                        Password = requser.Password
                    });
                }
            }

            UnitWork.Save();
            string[] orgIds = request.OrganizationIds.Split(',').ToArray();

            _revelanceApp.DeleteBy(Define.USERORG, requser.Id);
            _revelanceApp.Assign(Define.USERORG, orgIds.ToLookup(u => requser.Id));
        }

        public UserManagerApp(IUnitWork unitWork, IRepository<SysUser> repository,
            RevelanceManagerApp app,IAuth auth, OrgManagerApp orgManagerApp) : base(unitWork, repository, auth)
        {
            _revelanceApp = app;
            _orgManagerApp = orgManagerApp;
        }

        public void ChangePassword(ChangePasswordReq request)
        {
            Repository.Update(u => u.UserName == request.Account, user => new SysUser
            {
                Password = request.Password
            });
        }

        public TableData LoadByRole(QueryUserListByRoleReq request)
        {
            var users = from userRole in UnitWork.Find<Relevance>(u =>
                    u.TopFirstId == request.roleId && u.TopKey == Define.USERROLE)
                join user in UnitWork.Find<User>(null) on userRole.TopFirstId equals user.Id into temp
                from c in temp.Where(u =>u.Id != null)
                select c;

            return new TableData
            {
                count = users.Count(),
                data = users.Skip((request.page - 1) * request.limit).Take(request.limit)
            };
        }
        
        public TableData LoadByOrg(QueryUserListByOrgReq request)
        {
            var users = from userRole in UnitWork.Find<Relevance>(u =>
                    u.TopFirstId == request.orgId && u.TopKey == Define.USERORG)
                join user in UnitWork.Find<User>(null) on userRole.TopFirstId equals user.Id into temp
                from c in temp.Where(u =>u.Id != null)
                select c;

            return new TableData
            {
                count = users.Count(),
                data = users.Skip((request.page - 1) * request.limit).Take(request.limit)
            };
        }

        /// <summary>
        /// 修改用户资料
        /// </summary>
        /// <param name="request"></param>
        public void ChangeProfile(ChangeProfileReq request)
        {
            if (request.Account == Define.SYSTEM_USERNAME)
            {
                throw new Exception("不能修改超级管理员信息");
            }
            
            Repository.Update(u => u.UserName == request.Name, user => new SysUser
            {
                UserName = request.Name,
                            //Sex = request.Sex
                        });
        }
    }
}