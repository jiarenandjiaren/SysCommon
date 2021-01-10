/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 基础参数配置
* 类 名： Define
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

namespace SysCommon.Service
{
    public static class Define
    {
        public static string USERROLE = "UserRoleTop";       //用户角色关联KEY
        public const string ROLERESOURCE= "RoleResourceTop";  //角色资源关联KEY
        public const string USERORG = "UserOrgTop";  //用户机构关联KEY
        public const string ROLEELEMENT = "RoleElementTop"; //角色按钮关联KEY
        public const string ROLEMENU = "RoleMenuTop";   //角色模块关联KEY
        public const string ROLEDATAPROPERTY = "RoleDataPropertyTop";   //角色数据字段权限
        public const string MENUELEMENT = "MenuElementTop";   //栏目按钮权限

        public const string DBTYPE_SQLSERVER = "SqlServer";    //sql server
        public const string DBTYPE_MYSQL = "MySql";    //sql server


        public const int INVALID_TOKEN = 50014;     //token无效

        public const string TOKEN_NAME = "Authorization";


        public const string SYSTEM_USERNAME = "System";
        public const string SYSTEM_USERPWD = "123456";

        public const string DATAPRIVILEGE_LOGINUSER = "{loginUser}";  //数据权限配置中，当前登录用户的key
        public const string DATAPRIVILEGE_LOGINROLE = "{loginRole}";  //数据权限配置中，当前登录用户角色的key
        public const string DATAPRIVILEGE_LOGINORG = "{loginOrg}";  //数据权限配置中，当前登录用户部门的key

        public const string JOBMAPKEY = "OpenJob";

        /// <summary>
        /// token有效期(小时=默认10)
        /// </summary>
        public const int ExpHours = 10;






        public static string ArticleTypeLabel = "ArticleTypeLabelTop";       //文章标签类型
    }
}
