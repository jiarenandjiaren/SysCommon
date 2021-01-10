/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 文章分类服务
* 类 名： ArticleTypeService
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/12/01 17:00   N/A    初版
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
    public class ArticleTypeService : BaseService<Repository.Domain.ArticleType>
    {
        private readonly IAuth _authUtil;
        public ArticleTypeService(IUnitWork unitWork, IRepository<Repository.Domain.ArticleType> repository, IAuth auth) : base(unitWork, repository)
        {
            _authUtil = auth;
        }
        /// <summary>
        /// 添加文章类型
        /// </summary>
        /// <param name="aArticleTypeReq"></param>
        public void Add(AArticleTypeReq aArticleTypeReq)
        {
            Repository.Domain.ArticleType requser = aArticleTypeReq;
            if (Repository.GetAll().Any(u => u.TypeName == aArticleTypeReq.TypeName))
            {
                throw new ArgumentException("类型名称：" + aArticleTypeReq.TypeName + "已经存在");
            }
            //requser.CreateId = _authUtil.GetCurrentUser().User.Id;
            requser.CreateTime = DateTime.Now;
            Repository.Add(requser);
        }
        public void Edit(EArticleTypeReq eArticleTypeReq )
        {
            Repository.Domain.ArticleType requser = eArticleTypeReq;
            if (Repository.GetAll().Any(u => u.TypeName == eArticleTypeReq.TypeName && u.Id != eArticleTypeReq.Id))
            {
                throw new ArgumentException("类型名称：" + eArticleTypeReq.TypeName + "已经存在");
            }
            UnitWork.Update<Repository.Domain.ArticleType>(u => u.Id == requser.Id, u => new Repository.Domain.ArticleType
            {
                TypeName = requser.TypeName,
                IsEnable = requser.IsEnable,
                UpdateTime = DateTime.Now,
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
            UnitWork.Update<Repository.Domain.ArticleType>(u => u.Id == request.Id, u => new Repository.Domain.ArticleType
            {
                IsDelete = true
            });
        }
        /// <summary>
        /// 展示
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData Show(QueryArticleTypeListReq request)
        {
            TableData tableData = new TableData();
            try
            {
                var query = from article in UnitWork.Find<ArticleType>(u => u.IsDelete == false)
                            join adminC in UnitWork.Find<SysUser>(null)
                            on article.CreateId equals adminC.Id into c
                            from ci in c.DefaultIfEmpty()
                            join adminU in UnitWork.Find<SysUser>(null)
                            on article.UpdateId equals adminU.Id into u
                            from ui in u.DefaultIfEmpty()
                            select new
                            {
                                article.Id,
                                article.TypeName,
                                CreateName = ci.UserName,
                                article.CreateTime,
                                UpdateName = ui.UserName,
                                article.UpdateTime,
                                article.IsDelete,
                                article.IsEnable,
                                article.Sort,
                                article.Description,
                            };
                if (!string.IsNullOrEmpty(request.TypeName))//类型名称
                {
                    query = query.Where(u => u.TypeName.Contains(request.TypeName));
                }
                if (request.IsEnable == 1)//在用
                {
                    query = query.Where(u => u.IsEnable == true);
                }
                else if (request.IsEnable == 0)//停用
                {
                    query = query.Where(u => u.IsEnable == false);
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
                var query = UnitWork.Find<Repository.Domain.ArticleType>(u=>u.Id == request.Id);
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
        /// 展示所有数据
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData ShowAll()
        {
            TableData tableData = new TableData();
            try
            {
                var query = UnitWork.Find<Repository.Domain.ArticleType>(u => u.IsDelete == false&&u.IsEnable == true);
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
        /// 展示类型及对应标签
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData ShowTypeLabel()
        {
            TableData tableData = new TableData();
            try
            {
                var query = (from Art in UnitWork.Find<ArticleType>(u => u.IsDelete == false)
                            select new ArticleTypeView
                            {
                                UpdateId = Art.UpdateId,
                                UpdateTime = Art.UpdateTime,
                                CreateId = Art.CreateId,
                                CreateTime = Art.CreateTime,
                                Id =Art.Id,
                                Name = Art.TypeName,
                                IsEnable=Art.IsEnable,
                                Sort = Art.Sort,
                                Description = Art.Description,
                            }).ToList();
                var label = (from lab in UnitWork.Find<Label>(u => u.IsDelete == false)
                             select new LabelView
                             {
                                 Id = lab.Id,
                                 Name = lab.LabelName,
                                 TypeId = lab.TypeId,
                             }).ToList();
                foreach (var labs in query)
                {
                    labs.labels = label.Where(u => u.TypeId == labs.Id).ToList();
                }
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