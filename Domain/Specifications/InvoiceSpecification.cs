using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace Domain.Specifications
{
    public class InvoiceSpecification : Specification<Invoice>
    {
        private string _invoiceCodeNo;
        private string _drawer;
        private bool? _isRed;
        private Guid? _orderId;
        private DateTime? _startDate;
        private DateTime? _endDate;

        public InvoiceSpecification(
            string invoiceCodeNo = null,
            string drawer = null, 
            bool? isRed = null, 
            Guid? orderId = null,
            DateTime? startDate = null, 
            DateTime? endDate = null)
        {
            _invoiceCodeNo = invoiceCodeNo;
            _drawer = drawer;
            _isRed = isRed;
            _startDate = startDate;
            _orderId = orderId;
            _endDate = endDate;
        }

        public override Expression<Func<Invoice, bool>> ToExpression()
            => invoice =>
            invoice.IsValidate &&
            (_startDate == null || invoice.InvoiceDate >= _startDate) &&
            (_endDate == null || invoice.InvoiceDate <= _endDate) &&
            (_isRed == null || invoice.IsRed == _isRed) &&
            (string.IsNullOrWhiteSpace(_invoiceCodeNo) || invoice.InvoiceCode + invoice.InvoiceNo == _invoiceCodeNo) &&
            (string.IsNullOrWhiteSpace(_drawer) || invoice.Drawer.Contains(_drawer)) &&
            (_orderId == null || invoice.OrderId == _orderId);
    }
}
