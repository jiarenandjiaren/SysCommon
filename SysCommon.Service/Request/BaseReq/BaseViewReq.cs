/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： BaseAEntityReq
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
using SysCommon.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysCommon.Service.Request
{
    public abstract class BaseViewReq : BaseEntityReq
    {
        public BaseViewReq()
        {
            Id = string.Empty;
            CreateName = string.Empty;
            CreateTime = DateTime.Now;
            UpdateName = string.Empty;
            UpdateTime = DateTime.Now;
            IsDelete = false;
        }
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
