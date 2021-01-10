/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 标签
* 类 名： ALabelReq
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
    /// 添加文章属性
    /// </summary>
    public class ALabelReq : BaseAEntityReq
    {
        [Display(Name = "标签名称")]
        [Required(ErrorMessage = "标签名称不能为空")]
        public string LabelName { get; set; }
        [Display(Name = "类型id")]
        [Required(ErrorMessage = "类型id不能为空")]
        public string TypeId { get; set; }
        public static implicit operator ALabelReq(Repository.Domain.Label label)
        {
            return label.MapTo<ALabelReq>();
        }
        public static implicit operator Repository.Domain.Label(ALabelReq aLabelReq)
        {
            return aLabelReq.MapTo<Repository.Domain.Label>();
        }
        public ALabelReq()
        { }
    }
}