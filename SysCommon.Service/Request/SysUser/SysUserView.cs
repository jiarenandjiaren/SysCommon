/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： SysUserView
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/18 9:25   N/A    初版
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
    public class SysUserView : BaseViewReq
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImageUrl { get; set; }

        public static implicit operator SysUserView(SysUser user)
        {
            return user.MapTo<SysUserView>();
        }
        public static implicit operator SysUser(SysUserView view)
        {
            return view.MapTo<SysUser>();
        }
        public SysUserView()
        { }
    }
}