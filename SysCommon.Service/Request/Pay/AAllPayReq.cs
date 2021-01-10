/**  版本信息模板在安装目录下，可自行修改。
*
*
* 功 能： 
* 类 名： AAllPayReq
*
* Ver    变更日期             负责人 GYJ  变更内容
* ───────────────────────────────────
* V0.01  2020/8/31 11:45   N/A    初版
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
    public class AAllPayReq
    {

        /// <summary>
        /// 订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string orderName { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string orderAmount { get; set; }

        public static implicit operator AAllPayReq(Payment user)
        {
            return user.MapTo<AAllPayReq>();
        }
        public static implicit operator Payment(AAllPayReq view)
        {
            return view.MapTo<Payment>();
        }
        public AAllPayReq()
        { }
    }
}