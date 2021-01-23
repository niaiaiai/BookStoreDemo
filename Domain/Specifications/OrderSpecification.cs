using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace Domain.Specifications
{
    public class OrderSpecification : Specification<Order>
    {
        private bool? _isClose;
        private string _orderNo;
        private DateTime? _startTime;
        private DateTime? _endTime;
        private bool? _isDeliver;
        private bool? _isDone;
        private bool? _isNeedInvoice;

        public OrderSpecification(
            string orderNo = null,
            bool? isClose = null,
            bool? isDeliver = null, 
            bool? isDone = null, 
            bool? isNeedInvoice = null,
            DateTime? startTime = null,
            DateTime? endTime = null)
        {
            _orderNo = orderNo;
            _isClose = isClose;
            _startTime = startTime;
            _endTime = endTime;
            _isDeliver = isDeliver;
            _isDone = isDone;
            _isNeedInvoice = isNeedInvoice;
        }

        public override Expression<Func<Order, bool>> ToExpression()
            => order
            => (_startTime == null || order.OrderDate >= _startTime) && 
            (_endTime == null || order.OrderDate <= _endTime) &&
            (string.IsNullOrWhiteSpace(_orderNo) || order.OrderNo == _orderNo) &&
            (_isClose == null || order.IsClose == _isClose) &&
            (_isDeliver == null || order.IsDeliver == _isDeliver) &&
            (_isDone == null || order.IsDone == _isDone) &&
            (_isNeedInvoice == null || order.NeedInvoice == _isNeedInvoice);
    }
}
