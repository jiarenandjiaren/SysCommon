/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： ARoleMenuReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:34   N/A    初版
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
    public class ARoleMenuReq:BaseAEntityReq
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string[] MenuId { get; set; }
        public static implicit operator ARoleMenuReq(RoleMenuTop user)
        {
            return user.MapTo<ARoleMenuReq>();
        }
        public static implicit operator RoleMenuTop(ARoleMenuReq view)
        {
            return view.MapTo<RoleMenuTop>();
        }
        public ARoleMenuReq()
        { }
    }
}