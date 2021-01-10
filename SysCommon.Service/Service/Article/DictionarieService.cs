/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 字典服务
* 类 名： DictionarieService
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
    /// 字典服务
    /// </summary>
    public class DictionarieService : BaseService<Dictionarie>
    {
        private readonly IAuth _authUtil;
        public DictionarieService(IUnitWork unitWork, IRepository<Dictionarie> repository, IAuth auth) : base(unitWork, repository)
        {
            _authUtil = auth;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aArticleTypeReq"></param>
        public void Add(AArticleDataReq request,string articleid)
        {
            var dictionaries = new List<Dictionarie>();
            foreach (var labelid in request.LabelIds)
            {
                var TypeId = UnitWork.Find<Label>(u => u.Id == labelid).Select(u => u.TypeId).FirstOrDefault();//标签类型
                dictionaries.Add(new Dictionarie
                {
                    TopName = Define.ArticleTypeLabel,
                    TopFirstId = articleid,//文章id
                    TopSecondId = TypeId,//标签类型id
                    TopThirdId = labelid,//标签id
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                });
            }
            UnitWork.BatchAdd(dictionaries.ToArray());
            UnitWork.Save();
        }
        /// <summary>
        /// 修改关系
        /// </summary>
        /// <param name="aArticleTypeReq"></param>
        public void Edit(EArticleDataReq request, string articleid)
        {
            //重置之前的数据
            UnitWork.Delete<Dictionarie>(u => u.TopFirstId == articleid && u.TopName == Define.ArticleTypeLabel);
            var dictionaries = new List<Dictionarie>();
            foreach (var labelid in request.LabelIds)
            {
                var TypeId = UnitWork.Find<Label>(u => u.Id == labelid).Select(u=>u.TypeId).FirstOrDefault();//标签类型
                dictionaries.Add(new Dictionarie
                {
                    TopName = Define.ArticleTypeLabel,
                    TopFirstId = articleid,//文章id
                    TopSecondId = TypeId,//标签类型id
                    TopThirdId = labelid,//标签id
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                });
            }
            UnitWork.BatchAdd(dictionaries.ToArray());
            UnitWork.Save();
        }
    }
}