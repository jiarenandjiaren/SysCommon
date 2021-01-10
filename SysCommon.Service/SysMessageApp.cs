using System.Linq;
using Infrastructure;
using SysCommon.Service.Interface;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;


namespace SysCommon.Service
{
    public class SysMessageApp : BaseService<SysMessage>
    {
        private RevelanceManagerApp _revelanceApp;

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QuerySysMessageListReq request)
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

        public SysMessageApp(IUnitWork unitWork, IRepository<SysMessage> repository,
            RevelanceManagerApp app,IAuth auth) : base(unitWork, repository, auth)
        {
            _revelanceApp = app;
        }
    }
}