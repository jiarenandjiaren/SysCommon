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
    public class FrmLeaveReqApp : BaseApp<FrmLeaveReq,SysCommonDBContext>, ICustomerForm
    {
        private RevelanceManagerApp _revelanceApp;

        /// <summary>
        /// 加载列表
        /// </summary>
        public async Task<TableData> Load(QueryFrmLeaveReqListReq request)
        {
             return new TableData
            {
                count = Repository.Count(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public void Add(FrmLeaveReq obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(FrmLeaveReq obj)
        {
            UnitWork.Update<FrmLeaveReq>(u => u.Id == obj.Id, u => new FrmLeaveReq
            {
               //todo:要修改的字段赋值
            });

        }

        public FrmLeaveReqApp(IUnitWork<SysCommonDBContext> unitWork, IRepository<FrmLeaveReq,SysCommonDBContext> repository,
            RevelanceManagerApp app,IAuth auth) : base(unitWork, repository, auth)
        {
            _revelanceApp = app;
        }

        public void Add(string flowInstanceId, string frmData)
        {
            var req = JsonHelper.Instance.Deserialize<FrmLeaveReq>(frmData);
            req.FlowInstanceId = flowInstanceId;
            Add(req);
        }
    }
}