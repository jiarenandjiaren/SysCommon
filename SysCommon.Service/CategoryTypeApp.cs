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
    public class CategoryTypeApp : BaseService<CategoryType>
    {
        private RevelanceManagerApp _revelanceApp;

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryCategoryTypeListReq request)
        {
            var result = new TableData();
            var objs = UnitWork.Find<CategoryType>(null);
            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.Id.Contains(request.key) || u.Name.Contains(request.key));
            }

            result.data = objs.OrderBy(u => u.Name)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit);
            result.count = objs.Count();
            return result;
        }

        public void Add(AddOrUpdateCategoryTypeReq req)
        {
            var obj = req.MapTo<CategoryType>();
            //todo:补充或调整自己需要的字段
            obj.CreateTime = DateTime.Now;
            Repository.Add(obj);
        }

        public void Update(AddOrUpdateCategoryTypeReq obj)
        {
            var user = _auth.GetCurrentUser().User;
            UnitWork.Update<CategoryType>(u => u.Id == obj.Id, u => new CategoryType
            {
                Name = obj.Name,
                CreateTime = DateTime.Now
                //todo:补充或调整自己需要的字段
            });

        }

        public new void Delete(string[] ids)
        {
            UnitWork.Delete<CategoryType>(u => ids.Contains(u.Id));
            UnitWork.Delete<Category>(u => ids.Contains(u.TypeId));
            UnitWork.Save();
        }

        public CategoryTypeApp(IUnitWork unitWork, IRepository<CategoryType> repository,
            RevelanceManagerApp app, IAuth auth) : base(unitWork, repository, auth)
        {
            _revelanceApp = app;
        }
    }
}