/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 关联信息管理
* 类 名： ARelevanceReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 15:07   N/A    初版
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
    
    /// <summary>
    /// 角色分配用户
    /// </summary>
    public class ARoleUsers: BaseAEntityReq
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 用户id列表
        /// </summary>
        public string[] UserIds { get; set; }
    }
    /// <summary>
    /// 为角色分配数据字段权限
    /// </summary>
    public class ARelevanceRoleToPropertyReq: BaseAEntityReq
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 模块的Code,比如Category/Resource
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段名称列表
        /// </summary>
        public string[] Properties { get; set; }
    }
    /// <summary>
    /// 部门分配用户
    /// </summary>
    public class AOrgUsers: BaseAEntityReq
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public string OrgId { get; set; }
        /// <summary>
        /// 用户id列表
        /// </summary>
        public string[] UserIds { get; set; }
    }
    /// <summary>
    /// 角色分配栏目
    /// </summary>
    public class ARoleMenus : BaseAEntityReq
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 栏目id列表
        /// </summary>
        public string[] MenuIds { get; set; }
    }
    /// <summary>
    /// 角色分配按钮
    /// </summary>
    public class ARoleElement : BaseAEntityReq
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 按钮id列表
        /// </summary>
        public string[] ElementIds { get; set; }
    }
    /// <summary>
    /// 用户分配角色
    /// </summary>
    public class AUserRoles : BaseAEntityReq
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户id列表
        /// </summary>
        public string[] RoleIds { get; set; }
    }
}