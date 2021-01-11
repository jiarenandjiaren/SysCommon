using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysCommon.App.Interface;
using SysCommon.App.Request;
using SysCommon.Repository;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.App
{
    /// <summary>
    /// 分类管理
    /// </summary>
    public class AppManager : BaseApp<Application,SysCommonDBContext>
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


        public async Task<List<Application>> GetList(QueryAppListReq request)
        {
            var applications =  UnitWork.Find<Application>(null) ;
           
            return applications.ToList();
        }

        public AppManager(IUnitWork<SysCommonDBContext> unitWork, IRepository<Application,SysCommonDBContext> repository,IAuth auth) : base(unitWork, repository, auth)
        {
        }
    }
}