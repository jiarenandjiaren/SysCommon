/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 文章服务
* 类 名： ArticleDataService
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
    /// 文章服务
    /// </summary>
    public class ArticleDataService : BaseService<ArticleData>
    {
        private readonly IAuth _authUtil;
        private readonly DictionarieService _dictionarieService;
        public ArticleDataService(IUnitWork unitWork, IRepository<ArticleData> repository, IAuth auth, DictionarieService dictionarieService) : base(unitWork, repository)
        {
            _authUtil = auth;
            _dictionarieService = dictionarieService;
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="aArticleTypeReq"></param>
        public void Add(AArticleDataReq request)
        {
            ArticleData requser = request;
            if (Repository.GetAll().Any(u => u.Title == request.Title))
            {
                throw new ArgumentException("标题：" + request.Title + "已经存在");
            }
            requser.CreateId = _authUtil.GetCurrentUser().User.Id;
            requser.CreateTime = DateTime.Now;
            requser.UpdateTime = DateTime.Now;
            Repository.Add(requser);
            var articleid = UnitWork.Find<ArticleData>(u=>u.Title == request.Title).Select(u=>u.Id).FirstOrDefault().ToString();
            _dictionarieService.Add(request, articleid);
        }
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="eArticleTypeReq"></param>
        public void Edit(EArticleDataReq request)
        {
            ArticleData requser = request;
            if (Repository.GetAll().Any(u => u.Title == request.Title && u.Id != request.Id))
            {
                throw new ArgumentException("文章标题：" + request.Title + "已经存在");
            }
            UnitWork.Update<ArticleData>(u => u.Id == requser.Id, u => new ArticleData
            {
                Title = request.Title,
                PictureUrl = request.PictureUrl,
                IsPublic = request.IsPublic,
                Synopsis = request.Synopsis,
                Content = request.Content,
                UpdateTime = DateTime.Now,
                UpdateId = _authUtil.GetCurrentUser().User.Id,
                Sort = requser.Sort,
                Description = requser.Description
            });
            _dictionarieService.Edit(request, request.Id);
        }
        /// <summary>
        /// 删除（假删）
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public void Delete(QueryOneReq request)
        {
            UnitWork.Update<ArticleData>(u => u.Id == request.Id, u => new ArticleData
            {
                IsDelete = true
            });
            UnitWork.Delete<Dictionarie>(u => u.TopFirstId == request.Id && u.TopName == Define.ArticleTypeLabel);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public void Check(QueryCheckReq request)
        {
            //删除当前文章的标签
            UnitWork.Update<ArticleData>(u => u.Id == request.Id, u => new ArticleData
            {
                Check = request.Check
            });
        }
        /// <summary>
        /// 展示
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData Show(QueryArticleDataListReq request)
        {
            TableData tableData = new TableData();
            try
            {
                var query = (from art in UnitWork.Find<ArticleData>(u => u.IsDelete == false)//文章表                             
                             select new ArticleDataView
                             {
                                 Id = art.Id,
                                 Title = art.Title,
                                 PictureUrl = art.PictureUrl,
                                 IsPublic = art.IsPublic,
                                 Synopsis = art.Synopsis,
                                 Content = art.Content,
                                 Check = art.Check,
                                 CreateId = art.CreateId,
                                 CreateTime = art.CreateTime,
                                 UpdateId = art.UpdateId,
                                 UpdateTime = art.UpdateTime,
                                 IsEnable = art.IsEnable,
                                 Sort = art.Sort,
                                 Description = art.Description,
                                 
                             }).ToList();
                if(!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(u => u.Title.Contains(request.Title)).ToList();
                }
                if(request.Check == 0)//未审核
                    query = query.Where(u => u.Check == 0).ToList();
                else if(request.Check == 1)//通过
                    query = query.Where(u => u.Check == 1).ToList();
                else if (request.Check == 2)//不通过
                    query = query.Where(u => u.Check == 2).ToList();
                var dic = UnitWork.Find<Dictionarie>(
                     u =>
                         (u.TopName == Define.ArticleTypeLabel));
                foreach (var art in query)
                {
                    art.labelids = dic.Where(u=>u.TopFirstId == art.Id).Select(u=>u.TopThirdId).ToList();
                }
                tableData.count = query.Count();
               
                tableData.data = query;
            }
            catch (Exception ex)
            {
                tableData.Code = 102;
                tableData.message = ex.InnerException?.Message ?? ex.Message;
            }
            return tableData;
        }
        //public List<ArticleType> Type
        //{
        //    get
        //    {
        //        var elementIds = UnitWork.Find<Dictionarie>(
        //             u =>
        //                 (u.TopName == Define.ArticleTypeLabel)).Select(u => u.TopSecondId);
        //        var userelements = UnitWork.Find<ArticleType>(u => elementIds.Contains(u.Id));
        //        return userelements.ToList();
        //    }
        //}
        /// <summary>
        /// 展示单条数据
        /// </summary>
        /// <param name="updateAdminReq"></param>
        public TableData GetDataOne(QueryOneReq request)
        {
            TableData tableData = new TableData();
            try
            {
                var query = (from art in UnitWork.Find<ArticleData>(u => u.IsDelete == false&&u.Id == request.Id)//文章表                             
                             select new ArticleDataView
                             {
                                 Id = art.Id,
                                 Title = art.Title,
                                 PictureUrl = art.PictureUrl,
                                 IsPublic = art.IsPublic,
                                 Synopsis = art.Synopsis,
                                 Content = art.Content,
                                 Check = art.Check,
                                 CreateId = art.CreateId,
                                 CreateTime = art.CreateTime,
                                 UpdateId = art.UpdateId,
                                 UpdateTime = art.UpdateTime,
                                 IsEnable = art.IsEnable,
                                 Sort = art.Sort,
                                 Description = art.Description,

                             }).ToList();
                var dic = UnitWork.Find<Dictionarie>(
                     u =>
                         (u.TopName == Define.ArticleTypeLabel));
                foreach (var art in query)
                {
                    art.labelids = dic.Where(u => u.TopFirstId == art.Id).Select(u => u.TopThirdId).ToList();
                }
                tableData.count = query.Count();

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