/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 关联数据
* 类 名： Relevance
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 14:50   N/A    初版
*
* Copyright (c) 2020 Maticsoft Corporation. All rights reserved.
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
	/// 多对多关系集中映射
	/// </summary>
      [Table("Relevance")]
    public partial class Relevance : BaseEntity
    {
        public Relevance()
        {
            this.TopKey = string.Empty;
            this.TopFirstId = string.Empty;
            this.TopSecondId = string.Empty;
            this.TopThirdId = string.Empty;
        }

        /// <summary>
	    /// 关联Key
	    /// </summary>
         [Description("关联Key")]
        public string TopKey { get; set; }
        /// <summary>
        /// 关联Key
        /// </summary>
        [Description("关联一级")]
        public string TopFirstId { get; set; }
        /// <summary>
        /// 关联Key
        /// </summary>
        [Description("关联二级")]
        public string TopSecondId { get; set; }
        /// <summary>
        /// 关联Key
        /// </summary>
        [Description("关联三级")]
        public string TopThirdId { get; set; }
    }
}