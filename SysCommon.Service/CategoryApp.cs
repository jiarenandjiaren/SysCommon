using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using SysCommon.Service.Interface;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;


namespace SysCommon.Service
{
    public class CategoryApp : BaseService<Category>
    {
        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryCategoryListReq request)
        {
            var loginContext = _auth.GetCurrentUser();
            if (loginContext == null)
            {
                throw new CommonException("登录已过期", Define.INVALID_TOKEN);
            }

            var properties = loginContext.GetProperties("Category");

            if (properties == null || properties.Count == 0)
            {
                throw new Exception("当前登录用户没有访问该模块字段的权限，请联系管理员配置");
            }

            var result = new TableData();
            var objs = UnitWork.Find<Category>(null);
            if (!string.IsNullOrEmpty(request.TypeId))
            {
                objs = objs.Where(u => u.TypeId == request.TypeId);
            }

            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.Id.Contains(request.key) || u.Name.Contains(request.key));
            }

            var propertyStr = string.Join(',', properties.Select(u => u.Key));
            result.columnHeaders = properties;
            result.data = objs.OrderBy(u => u.DtCode)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit).Select($"new ({propertyStr})");
            result.count = objs.Count();
            return result;
        }

        public void Add(AddOrUpdateCategoryReq req)
        {
            var obj = req.MapTo<Category>();
            obj.CreateTime = DateTime.Now;
            var user = _auth.GetCurrentUser().User;
            obj.CreateUserId = user.Id;
            obj.CreateUserName = user.UserName;
            Repository.Add(obj);
        }

        public void Update(AddOrUpdateCategoryReq obj)
        {
            var user = _auth.GetCurrentUser().User;
            UnitWork.Update<Category>(u => u.Id == obj.Id, u => new Category
            {
                Enable = obj.Enable,
                DtValue = obj.DtValue,
                DtCode = obj.DtCode,
                TypeId = obj.TypeId,
                UpdateTime = DateTime.Now,
                UpdateUserId = user.Id,
                UpdateUserName = user.UserName
                //todo:要修改的字段赋值
            });

        }

        public List<CategoryType> AllTypes()
        {
            return UnitWork.Find<CategoryType>(null).ToList();
        }

        public CategoryApp(IUnitWork unitWork, IRepository<Category> repository, IAuth auth) : base(unitWork, repository, auth)
        {
        }
    }
}