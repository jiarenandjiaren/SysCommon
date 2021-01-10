using System;
using System.Linq;
using Infrastructure;
using SysCommon.Service.Interface;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;


namespace SysCommon.Service
{
    public class WmsInboundOrderDtblApp : BaseService<WmsInboundOrderDtbl>
    {
        private RevelanceManagerApp _revelanceApp;
        private DbExtension _dbExtension;

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryWmsInboundOrderDtblListReq request)
        {

            var loginContext = _auth.GetCurrentUser();
            if (loginContext == null)
            {
                throw new CommonException("登录已过期", Define.INVALID_TOKEN);
            }
            
            //todo:普通账号如何分配明细的字段？？？？先写死😰

            var properties = _dbExtension.GetProperties("WmsInboundOrderDtbl");
            
            var result = new TableData();
            var objs = UnitWork.Find<WmsInboundOrderDtbl>(null);
            if (!string.IsNullOrEmpty(request.InboundOrderId))
            {
                objs = objs.Where(u => u.OrderId == request.InboundOrderId);
            }
            
            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.GoodsId.Contains(request.key));
            }

            var propertyStr = string.Join(',', properties.Select(u => u.Key));
            result.columnHeaders = properties;
            result.data = objs.OrderBy(u => u.Id)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit).Select($"new ({propertyStr})");
            result.count = objs.Count();
            return result;
        }

        public void Add(AddOrUpdateWmsInboundOrderDtblReq req)
        {
            AddNoSave(req);
            UnitWork.Save();
        }
        
        public void AddNoSave(AddOrUpdateWmsInboundOrderDtblReq req)
        {
            var obj = req.MapTo<WmsInboundOrderDtbl>();
            //todo:补充或调整自己需要的字段
            obj.CreateTime = DateTime.Now;
            var user = _auth.GetCurrentUser().User;
            obj.CreateUserId = user.Id;
            obj.CreateUserName = user.UserName;
            UnitWork.Add(obj);
        }

         public void Update(AddOrUpdateWmsInboundOrderDtblReq obj)
        {
            var user = _auth.GetCurrentUser().User;
            UnitWork.Update<WmsInboundOrderDtbl>(u => u.Id == obj.Id, u => new WmsInboundOrderDtbl
            {
                Price = obj.Price,
                PriceNoTax = obj.PriceNoTax,
                InStockStatus = obj.InStockStatus,
                AsnStatus = obj.AsnStatus,
                GoodsId = obj.GoodsId,
                GoodsBatch = obj.GoodsBatch,
                QualityFlg = obj.QualityFlg,
                OrderNum = obj.OrderNum,
                InNum = obj.InNum,
                LeaveNum = obj.LeaveNum,
                HoldNum = obj.HoldNum,
                ProdDate = obj.ProdDate,
                ExpireDate = obj.ExpireDate,
                TaxRate = obj.TaxRate,
                OwnerId = obj.OwnerId,
                Remark = obj.Remark,
                UpdateTime = DateTime.Now,
                UpdateUserId = user.Id,
                UpdateUserName = user.UserName
                //todo:补充或调整自己需要的字段
            });

        }

        public WmsInboundOrderDtblApp(IUnitWork unitWork, IRepository<WmsInboundOrderDtbl> repository,
            RevelanceManagerApp app, IAuth auth, DbExtension dbExtension) : base(unitWork, repository,auth)
        {
            _dbExtension = dbExtension;
            _revelanceApp = app;
        }
    }
}