using System;
using System.Collections.Generic;
using System.Linq;
using SysCommon.Service.Interface;
using SysCommon.Service.Request;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.Service
{
    /// <summary>
    /// 分类管理
    /// </summary>
    public class AppManager : BaseService<Application>
    {
        public void Add(Application Application)
        {
            if (string.IsNullOrEmpty(Application.Id))
            {
                Application.Id = Guid.NewGuid().ToString();
            }
            Repository.Add(Application);
        }

        public void Update(Application Application)
        {
            Repository.Update(Application);
        }


        public List<Application> GetList(QueryAppListReq request)
        {
            var applications = UnitWork.Find<Application>(null);

            return applications.ToList();
        }

        public AppManager(IUnitWork unitWork, IRepository<Application> repository, IAuth auth) : base(unitWork, repository, auth)
        {
        }
    }
}