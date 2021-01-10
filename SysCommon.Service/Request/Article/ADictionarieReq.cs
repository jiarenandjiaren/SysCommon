/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 字典管理
* 类 名： ADictionarieReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:25   N/A    初版
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Infrastructure;
using SysCommon.Repository.Core;
using SysCommon.Repository.Domain;

namespace SysCommon.Service.Request
{
    /// <summary>
    /// 文章
    /// </summary>
    public class ADictionarieReq : BaseAEntityReq
    {
        /// <summary>
        /// 关联描述（A表关联B表   ABTop）
        /// </summary>
        [Description("关联描述（A表关联B表   ABTop）")]
        public string TopName { get; set; }
        /// <summary>
        /// A表关联B表  （A表Id）
        /// </summary>
        [Description("A表关联B表  （A表Id）")]
        public string TopFirstId { get; set; }
        /// <summary>
        /// A表关联B表  （B表Id）
        /// </summary>
        [Description("A表关联B表  （B表Id）")]
        public List<string> TopSecondId { get; set; }
        public List<string> TopThirdId { get; set; }


        public static implicit operator ADictionarieReq(Dictionarie dictionarie)
        {
            return dictionarie.MapTo<ADictionarieReq>();
        }
        public static implicit operator Dictionarie(ADictionarieReq aDictionarieReq)
        {
            return aDictionarieReq.MapTo<Dictionarie>();
        }
        public ADictionarieReq()
        { }
    }
}