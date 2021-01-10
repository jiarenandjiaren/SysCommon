using Infrastructure;
using SysCommon.Service.Interface;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;


namespace SysCommon.Service
{
    public class FrmLeaveReqApp : BaseService<FrmLeaveReq>, ICustomerForm
    {
        private RevelanceManagerApp _revelanceApp;

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryFrmLeaveReqListReq request)
        {
            return new TableData
            {
                count = Repository.GetCount(null),
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

        public FrmLeaveReqApp(IUnitWork unitWork, IRepository<FrmLeaveReq> repository,
            RevelanceManagerApp app, IAuth auth) : base(unitWork, repository, auth)
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