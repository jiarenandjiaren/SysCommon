/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 文章分类
* 类 名： ArticleType
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/11/30 09:06   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SysCommon.Repository.Core;

namespace SysCommon.Repository.Domain
{
    /// <summary>
	/// 文章分类
	/// </summary>
      [Table("ArticleType")]
    public partial class ArticleType : BaseEntity
    {
        public ArticleType()
        {
            this.TypeName = string.Empty;
        }
        [Description("分类名称")]
        public string TypeName { get; set; }
    }
}