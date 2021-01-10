/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： AExtendFieldReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:31   N/A    初版
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
    public class AElementReq: BaseAEntityReq
    {
        /// <summary>
        /// DOM ID
        /// </summary>
        public string DomId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 元素调用脚本
        /// </summary>
        public string Script { get; set; }
        /// <summary>
        /// 元素图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 元素样式
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 功能模块Id
        /// </summary>
        public string MenuId { get; set; }

        public static implicit operator AElementReq(Element user)
        {
            return user.MapTo<AElementReq>();
        }
        public static implicit operator Element(AElementReq view)
        {
            return view.MapTo<Element>();
        }
    }
}