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
	/// 表单模板表
	/// </summary>
      [Table("AdminRoleTop")]
    public partial class AdminRoleTop : BaseEntity
    {
        public AdminRoleTop()
        {
            this.RoleId = string.Empty;
            this.AdminId = string.Empty;
        }

        [Description("角色Id")]
        public string RoleId { get; set; }
        public string AdminId { get; set; }
    }
}