/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 文章类型请求参数
* 类 名： AArticleTypeReq
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Infrastructure;
using SysCommon.Repository.Core;
using SysCommon.Repository.Domain;

namespace SysCommon.Service.Request
{
    public class AArticleTypeReq: BaseAEntityReq
    {
        /// <summary>
        /// 文章类型名称
        /// </summary>
        public string TypeName { get; set; }


        public static implicit operator AArticleTypeReq(ArticleType  articleType)
        {
            return articleType.MapTo<AArticleTypeReq>();
        }
        public static implicit operator Repository.Domain.ArticleType(AArticleTypeReq aArticleTypeReq)
        {
            return aArticleTypeReq.MapTo<Repository.Domain.ArticleType>();
        }
        public AArticleTypeReq()
        { }
    }
}