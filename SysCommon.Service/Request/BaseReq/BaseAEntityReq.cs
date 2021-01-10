/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 展示数据基本类
* 类 名： BaseViewReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/9/16 9:16   N/A    初版
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
    public abstract class BaseAEntityReq : BaseEntityReq
    {
        public BaseAEntityReq()
        {
            //this.CreateId = string.Empty;
        }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public string CreateId { get; set; }
    }
}
