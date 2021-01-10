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
    /// 获取操作人信息
    /// </summary>
    public class CreateEditNameService : BaseService<Repository.Domain.SysUser>
    {
        public CreateEditNameService(IUnitWork unitWork, IRepository<Repository.Domain.SysUser> repository) : base(unitWork, repository)
        {
        }
        /// <summary>
        /// 加载操作人信息
        /// </summary>
        /// <param name="userId"></param>
        public List<Repository.Domain.SysUser> LoadForAdmin(string adminId)
        {
            var result = from a in UnitWork.Find<Repository.Domain.SysUser>(null)
                         where a.Id == adminId
                         select a;
            return result.ToList();
        }
    }
}