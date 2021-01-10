﻿﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
//     Author:Yubao Li
// </autogenerated>
//------------------------------------------------------------------------------
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
    public class EElementReq : BaseEEntityReq
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
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

        public static implicit operator EElementReq(Element user)
        {
            return user.MapTo<EElementReq>();
        }
        public static implicit operator Element(EElementReq view)
        {
            return view.MapTo<Element>();
        }
    }
}