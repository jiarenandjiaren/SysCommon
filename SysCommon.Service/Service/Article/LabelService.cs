/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 标签
* 类 名： LabelService
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/12/02 8:39   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
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
    /// 文章分类服务
    /// </summary>
    public class LabelService : BaseService<Repository.Domain.Label>
    {
        private readonly IAuth _authUtil;
        public LabelService(IUnitWork unitWork, IRepository<Repository.Domain.Label> repository, IAuth auth) : base(unitWork, repository)
        {
            _authUtil = auth;
        }
        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="aArticleTypeReq"></param>
        public void Add(ALabelReq aLabelReq)
        {
            Repository.Domain.Label requser = aLabelReq;
            if (UnitWork.Find<Repository.Domain.Label>(u => u.LabelName == aLabelReq.LabelName).Count()>0)
            {
                throw new ArgumentException("标签名称：" + aLabelReq.LabelName + "已经存在");
            }
            //requser.CreateId = _authUtil.GetCurrentUser().User.Id;
            requser.CreateTime = DateTime.Now;
            Repository.Add(requser);
        }
        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="eArticleTypeReq"></param>
        public void Edit(ELabelReq eLabelReq)
        {
            Repository.Domain.Label requser = eLabelReq;
            if (UnitWork.Find<Repository.Domain.Label>(u => u.LabelName == eLabelReq.LabelName && u.Id != eLabelReq.Id).Count() > 0)
            {
                throw new ArgumentException("标签名称：" + eLabelReq.LabelName + "已经存在");
            }
            UnitWork.Update<Repository.Domain.Label>(u => u.Id == requser.Id, u => new Repository.Domain.Label
            {
                LabelName = requser.LabelName,
                IsEnable = requser.IsEnable,
                UpdateTime = DateTime.Now,
                TypeId = eLabelReq.TypeId,
                //UpdateId = _authUtil.GetCurrentUser().User.Id,
                Sort = requser.Sort,
                Description = requser.Description
            });
        }
        /// <summary>
        /// 删除（假删）
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public void Delete(QueryOneReq request)
        {
            UnitWork.Update<Repository.Domain.Label>(u => u.Id == request.Id, u => new Repository.Domain.Label
            {
                IsDelete = true
            });
        }
        /// <summary>
        /// 展示
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData Show(QueryLabelListReq request)
        {
            TableData tableData = new TableData();
            try
            {
                var query = from lab in UnitWork.Find<Repository.Domain.Label>(u => u.IsDelete == false)
                            join suC in UnitWork.Find<SysUser>(null) on new { CreateId = lab.CreateId } equals new { CreateId = suC.Id } into suC_join
                            from suC in suC_join.DefaultIfEmpty()
                            join suU in UnitWork.Find<SysUser>(null) on new { UpdateId = lab.UpdateId } equals new { UpdateId = suU.Id } into suU_join
                            from suU in suU_join.DefaultIfEmpty()
                            join type in UnitWork.Find<Repository.Domain.ArticleType>(null) on new { TypeId = lab.TypeId } equals new { TypeId = type.Id } into type_join
                            from type in type_join.DefaultIfEmpty()
                            select new
                            {
                                Id = lab.Id,
                                LabelName = lab.LabelName,
                                TypeId = lab.TypeId,
                                CreateId = lab.CreateId,
                                CreateTime = lab.CreateTime,
                                UpdateId = lab.UpdateId,
                                UpdateTime = lab.UpdateTime,
                                IsEnable = lab.IsEnable,
                                IsDelete = lab.IsDelete,
                                Sort = lab.Sort,
                                Description = lab.Description,
                                Column1 = suC.Id,
                                Column2 = suU.Id,
                                Column3 = type.Id,
                                TypeName = type.TypeName
                            };
                if(!string.IsNullOrEmpty(request.LabelName))
                {
                    query = query.Where(u => u.LabelName.Contains(request.LabelName));
                }
                if(!string.IsNullOrEmpty(request.TypeId))
                {
                    query = query.Where(u => u.TypeId.Contains(request.TypeId));
                }
                if(request.IsEnable ==0)
                {
                    query = query.Where(u => u.IsEnable == false);
                }
                else if (request.IsEnable == 1)
                {
                    query = query.Where(u => u.IsEnable == true);
                }
                tableData.count = query.Count();
                query = query
                    .OrderBy(u => u.Sort)
                    .Skip((request.page - 1) * request.limit)
                    .Take(request.limit);
                tableData.data = query;
            }
            catch (Exception ex)
            {
                tableData.Code = 102;
                tableData.message = ex.InnerException?.Message ?? ex.Message;
            }
            return tableData;
        }
        /// <summary>
        /// 展示单条数据
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData GetDataOne(QueryOneReq request)
        {
            TableData tableData = new TableData();
            try
            {
                var query = UnitWork.Find<Repository.Domain.Label>(u=>u.Id == request.Id);
                tableData.data = query;
            }
            catch (Exception ex)
            {
                tableData.Code = 102;
                tableData.message = ex.InnerException?.Message ?? ex.Message;
            }
            return tableData;
        }
    }
}