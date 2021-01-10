/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： ESysOrgReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 11:37   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：九博科技有限公司　　　　　　　　　　　　　　            │
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using Infrastructure;
using SysCommon.Repository.Domain;

namespace SysCommon.Service.Request
{
    public partial class ESysOrgReq : BaseEEntityReq
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public string FatherId { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 组织图标
        /// </summary>
        public string IconName { get; set; }

        public static implicit operator ESysOrgReq(SysOrg sysOrg)
        {
            return sysOrg.MapTo<ESysOrgReq>();
        }
        public static implicit operator SysOrg(ESysOrgReq view)
        {
            return view.MapTo<SysOrg>();
        }
        public ESysOrgReq()
        { }
    }
}