using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using SysCommon.App.Interface;
using SysCommon.App.Request;
using SysCommon.App.Response;
using SysCommon.Repository;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;


namespace SysCommon.App
{
    public class SysMessageApp : BaseApp<SysMessage,SysCommonDBContext>
    {
        private RevelanceManagerApp _revelanceApp;

        /// <summary>
        /// 加载列表
        /// </summary>
        public async Task<TableData> Load(QuerySysMessageListReq request)
        {
            var loginContext = _auth.GetCurrentUser();
            if (loginContext == null)
            {
                throw new CommonException("登录已过期", Define.INVALID_TOKEN);
            }

            var result = new TableData();
            var objs = UnitWork.Find<SysMessage>(u =>u.ToId == loginContext.User.Id);

            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.Title.Contains(request.key) || u.Id.Contains(request.key));
            }

            result.data = objs.OrderBy(u => u.Id)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit);
            result.count = objs.Count();
            return result;
        }

        public void Add(SysMessage obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(SysMessage obj)
        {
            UnitWork.Update<SysMessage>(u => u.Id == obj.Id, u => new SysMessage
            {
               //todo:要修改的字段赋值
            });

        }

        public SysMessageApp(IUnitWork<SysCommonDBContext> unitWork, IRepository<SysMessage,SysCommonDBContext> repository,
            RevelanceManagerApp app,IAuth auth) : base(unitWork, repository, auth)
        {
            _revelanceApp = app;
        }
    }
}