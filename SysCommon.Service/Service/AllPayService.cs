using System;
using System.Collections.Generic;
using SysCommon.Service.Interface;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;
using System.Linq;
using SysCommon.Service.Request;
using System.Text;
using SysCommon.Repository;

namespace SysCommon.Service
{
    public class AllPayService : BaseService<Payment>
    {
        public AllPayService(IUnitWork unitWork, IRepository<Payment> repository) : base(unitWork, repository)
        {
        }
        public void Add(Payment payment)
        {
            if (Repository.GetAll().Any(u => u.OutTradeNo  == payment.OutTradeNo))
            {
                throw new ArgumentException("订单：" + payment.OutTradeNo + "已经存在");
            }
            else
                Repository.Add(payment);
        }
        public void Edit(Payment payment)
        {
            UnitWork.Update<Payment>(u => u.OutTradeNo == payment.OutTradeNo, u => new Payment
            {
                UpdateTime = payment.UpdateTime,
                OutTradeNo = payment.OutTradeNo,
                PayType = payment.PayType,
                BuyerLogonId = payment.BuyerLogonId,
                TradeNo = payment.TradeNo,
                TotalAmount = payment.TotalAmount,
                SendPayDate = payment.SendPayDate,
                ReceiptAmount = payment.ReceiptAmount,
                PointAmount = payment.PointAmount,
                TradeStatus = payment.TradeStatus,
                InvoiceAmount = payment.InvoiceAmount,
                FundBillList = payment.FundBillList,
                BuyerUserId = payment.BuyerUserId,
                BuyerPayAmount = payment.BuyerPayAmount,
                Msg = payment.Msg,
                ByUserType = payment.ByUserType,
                PayState = payment.PayState
            });
        }
        public void Refund(Payment payment)
        {
            UnitWork.Update<Payment>(u => u.OutTradeNo == payment.OutTradeNo, u => new Payment
            {
                send_back_fee = payment.send_back_fee,
                RefundNo = payment.RefundNo,
                gmt_refund_pay = payment.gmt_refund_pay,
                RefundState = payment.RefundState
            });
        }
    }
}