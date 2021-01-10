/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： BaseEntityReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:48   N/A    初版
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

namespace SysCommon.Service.Request
{
    public abstract class BaseEntityReq
    {
        public BaseEntityReq()
        {
            this.IsEnable = true;
            this.Sort = 0;
            //this.Description = string.Empty;
        }
        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 排序标识
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Description { get; set; }
    }
}
