﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
//     Author:Yubao Li
// </autogenerated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysCommon.Service.Request
{
    /// <summary>
	/// 入库通知单（入库订单）
	/// </summary>
    [Table("WmsInboundOrderTbl")]
    public partial class AddOrUpdateWmsInboundOrderTblReq 
    {

        /// <summary>
        /// 入库通知单号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 相关单据号
        /// </summary>
        public string ExternalNo { get; set; }
        /// <summary>
        /// 相关单据类型
        /// </summary>
        public string ExternalType { get; set; }
        /// <summary>
        /// 入库通知单状态(SYS_INSTCINFORMSTATUS)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 入库类型(SYS_INSTCTYPE)
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 商品类别
        /// </summary>
        public string GoodsType { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurchaseNo { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string StockId { get; set; }
        /// <summary>
        /// 货主编号(固定值CQM)
        /// </summary>
        public string OwnerId { get; set; }
        /// <summary>
        /// 承运人编号
        /// </summary>
        public string ShipperId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SupplierId { get; set; }
        /// <summary>
        /// 预定入库时间
        /// </summary>
        public System.DateTime? ScheduledInboundTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 承运方式
        /// </summary>
        public string TransferType { get; set; }
        /// <summary>
        /// 是否入保税库(0:否,1:是)
        /// </summary>
        public bool InBondedArea { get; set; }
        /// <summary>
        /// 销退箱数
        /// </summary>
        public decimal ReturnBoxNum { get; set; }
        
        /// <summary>
        /// 所属部门
        /// </summary>
        public string OrgId { get; set; }
        
        public List<AddOrUpdateWmsInboundOrderDtblReq> WmsInboundOrderDtblReqs { get; set; }
    }
}