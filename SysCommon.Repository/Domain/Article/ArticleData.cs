/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 文章数据
* 类 名： ArticleData
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/12/02 17:24   N/A    初版
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
	/// 文章数据
	/// </summary>
      [Table("ArticleData")]
    public partial class ArticleData : BaseEntity
    {
        public ArticleData()
        {
            this.Title = string.Empty;
            this.PictureUrl = string.Empty;
            this.IsPublic = true;
            this.Synopsis = string.Empty;
            this.Content = string.Empty;
            this.Check = 0;
        }
        [Description("主题")]
        public string Title { get; set; }
        [Description("主图路径")]
        public string PictureUrl { get; set; }
        [Description("是否公开")]
        public bool IsPublic { get; set; }
        [Description("简介")]
        public string Synopsis { get; set; }
        [Description("文章内容")]
        public string Content { get; set; }
        [Description("审核（0：未审核   1：通过   2：不通过）")]
        public int Check { get; set; }
    }
}