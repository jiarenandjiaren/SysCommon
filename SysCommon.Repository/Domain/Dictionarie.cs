/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 字典
* 类 名： Dictionarie
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/12/02 17:23   N/A    初版
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
    /// 字典
    /// </summary>
    [Table("Dictionarie")]
    public partial class Dictionarie : BaseEntity
    {
        public Dictionarie()
        {
          this.TopName = string.Empty;
          this.TopFirstId = string.Empty;
          this.TopSecondId = string.Empty; 
          this.TopThirdId = string.Empty; 
        }
        /// <summary>
        /// 关联描述（A表关联B表   ABTop）
        /// </summary>
        [Description("关联描述（A表关联B表   ABTop）")]
        public string TopName { get; set; }
        /// <summary>
        /// A表关联B表  （A表Id）
        /// </summary>
        [Description("A表关联B表  （A表Id）")]
        public string TopFirstId { get; set; }
        /// <summary>
        /// A表关联B表  （B表Id）
        /// </summary>
        [Description("A表关联B表  （B表Id）")]
        public string TopSecondId { get; set; }
        /// <summary>
        /// A表关联B表  （B表Id）
        /// </summary>
        [Description("A表关联B表  （B表Id）")]
        public string TopThirdId { get; set; }
    }
}