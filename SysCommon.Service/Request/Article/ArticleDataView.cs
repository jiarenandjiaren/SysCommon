/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 文章
* 类 名： AArticleReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/11/30 9:14   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Infrastructure;
using Newtonsoft.Json;
using SysCommon.Repository.Core;
using SysCommon.Repository.Domain;

namespace SysCommon.Service.Request
{
    public class ArticleDataView : BaseEEntityReq
    {
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateName { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateName { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 主图路径
        /// </summary>
        public string PictureUrl { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Synopsis { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> labelids { get; set; }
        public int Check { get; set; }


        public static implicit operator ArticleDataView(ArticleData articleData)
        {
            return articleData.MapTo<ArticleDataView>();
        }
        public static implicit operator ArticleData(ArticleDataView aArticleReq)
        {
            return aArticleReq.MapTo<ArticleData>();
        }
        public ArticleDataView()
        { }
    }
}