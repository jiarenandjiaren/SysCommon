﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
//     Author:Yubao Li
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SysCommon.Repository.Core;

namespace SysCommon.Repository.Domain
{
    /// <summary>
	/// 内容替换
	/// </summary>
      [Table("ContentReplace")]
    public partial class ContentReplace : Entity
    {
        public ContentReplace()
        {
            this.TableName = string.Empty;
            this.FieldName = string.Empty;
            this.OldContent = string.Empty;
            this.NewContent = string.Empty;
            this.CreateId = string.Empty;
            this.CreateDate = DateTime.Now;
            this.Description = string.Empty;
        }
        [Description("数据表名称")]
        public string TableName { get; set; }
        [Description("字段名称")]
        public string FieldName { get; set; }
        [Description("被替换内容")]
        public string OldContent { get; set; }
        [Description("替换内容")]
        public string NewContent { get; set; }
        [Description("创建人Id")]
        public string CreateId { get; set; }
        [Description("创建时间")]
        public DateTime? CreateDate { get; set; }
        [Description("备注")]
        public string Description { get; set; }
    }
}