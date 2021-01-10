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
    public class AArticleReq : BaseAEntityReq
    {
        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "主题能为空")]
        [MaxLength(500, ErrorMessage = "标题最大长度500")]
        public string Title { get; set; }
        /// <summary>
        /// 主图路径
        /// </summary>
        [MaxLength(50, ErrorMessage = "标题最大长度50")]
        public string PictureUrl { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(500, ErrorMessage = "标题最大长度500")]
        public string Synopsis { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }


        public static implicit operator AArticleReq(ArticleData articleData)
        {
            return articleData.MapTo<AArticleReq>();
        }
        public static implicit operator ArticleData(AArticleReq aArticleReq)
        {
            return aArticleReq.MapTo<ArticleData>();
        }
        public AArticleReq()
        { }
    }
}