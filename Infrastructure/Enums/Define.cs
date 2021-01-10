/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 共用信息配置
* 类 名： Define
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:46   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Enums
{
    public struct Define
    {
        public const string USERROLE = "UserRoleTop";      //用户角色关联KEY
        //public const string ROLERESOURCE = "RoleResource";  //角色资源关联KEY
        public const string USERORG = "UserOrgTop";  //用户组织关联KEY
        public const string ROLEELEMENT = "RoleElementTop"; //角色按钮关联KEY
        public const string ROLEMODULE = "RolMenuTop";   //角色栏目（模块）关联KEY
        public const string ROLEDATAPROPERTY = "RoleDataPropertytop";   //角色数据字段权限
        public const string ROLEMENU = "RoleMenuTop";
        public const string ROLERESOURCE = "";

        public const string DBTYPE_SQLSERVER = "SqlServer";    //sql server
        public const string DBTYPE_MYSQL = "MySql";    //sql server


        public const int INVALID_TOKEN = 50014;     //token无效

        public const string TOKEN_NAME = "X-Token";


        public const string SYSTEM_USERNAME = "System";
        public const string SYSTEM_USERPWD = "123456";

        public const string DATAPRIVILEGE_LOGINUSER = "{loginUser}";  //数据权限配置中，当前登录用户的key
        public const string DATAPRIVILEGE_LOGINROLE = "{loginRole}";  //数据权限配置中，当前登录用户角色的key
        public const string DATAPRIVILEGE_LOGINORG = "{loginOrg}";  //数据权限配置中，当前登录用户部门的key

        public const string JOBMAPKEY = "OpenJob";
    }
}
