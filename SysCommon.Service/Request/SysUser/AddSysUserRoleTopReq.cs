﻿﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
//     Author:Yubao Li
// </autogenerated>
//------------------------------------------------------------------------------
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
    public class AddSysUserRoleTopReq
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        public static implicit operator AddSysUserRoleTopReq(AdminRoleTop user)
        {
            return user.MapTo<AddSysUserRoleTopReq>();
        }
        public static implicit operator AdminRoleTop(AddSysUserRoleTopReq view)
        {
            return view.MapTo<AdminRoleTop>();
        }
        public AddSysUserRoleTopReq()
        { }
    }
}