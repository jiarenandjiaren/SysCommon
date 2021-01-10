using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using SysCommon.Service.Interface;
using SysCommon.Service.Request;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.Service
{
    public class OrgManagerApp : BaseService<SysOrg>
    {
        private RevelanceManagerApp _revelanceApp;
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="org">The org.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.Exception">未能找到该组织的父节点信息</exception>
        public string Add(SysOrg org)
        {
            var loginContext = _auth.GetCurrentUser();
            if (loginContext == null)
            {
                throw new CommonException("登录已过期", Define.INVALID_TOKEN);
            }
            //ChangeModuleCascade(org);

            Repository.Add(org);
            
            //如果当前账号不是SYSTEM，则直接分配
            var loginUser = _auth.GetCurrentUser();
            if (loginUser.User.UserName != Define.SYSTEM_USERNAME)
            {
                _revelanceApp.Assign(new AssignReq
                {
                    type=Define.USERORG,
                    firstId = loginContext.User.Id,
                    secIds = new[]{org.Id}
                });
            }
            
            return org.Id;
        }

        public string Update(SysOrg org)
        {
            //ChangeModuleCascade(org);

            //获取旧的的CascadeId
            var cascadeId = Repository.FindSingle(o => o.Id == org.Id).UpdateId;
            //根据CascadeId查询子部门
            var orgs = Repository.Find(u => u.UpdateId.Contains(cascadeId) && u.Id != org.Id)
            .OrderBy(u => u.UpdateId).ToList();

            //更新操作
            UnitWork.Update(org);

            //更新子部门的CascadeId
            foreach (var a in orgs)
            {
               // ChangeModuleCascade(a);
                UnitWork.Update(a);
            }

            UnitWork.Save();

            return org.Id;
        }

        /// <summary>
        /// 删除指定ID的部门及其所有子部门
        /// </summary>
        public void DelOrg(string[] ids)
        {
            var delOrg = Repository.Find(u => ids.Contains(u.Id)).ToList();
            foreach (var org in delOrg)
            {
                Repository.Delete(u => u.UpdateId.Contains(org.UpdateId));
            }
        }

        /// <summary>
        /// 加载特定用户的部门
        /// </summary>
        /// <param name="userId">The user unique identifier.</param>
        public List<SysOrg> LoadForUser(string userId)
        {
            var result = from userorg in UnitWork.Find<Relevance>(null)
                join org in UnitWork.Find<SysOrg>(null) on userorg.TopFirstId equals org.Id
                where userorg.TopFirstId == userId && userorg.TopKey == Define.USERORG
                select org;
            return result.ToList();
        }

        public OrgManagerApp(IUnitWork unitWork, IRepository<SysOrg> repository,IAuth auth, 
            RevelanceManagerApp revelanceApp) : base(unitWork, repository, auth)
        {
            _revelanceApp = revelanceApp;
        }
    }
}