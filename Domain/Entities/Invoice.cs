using MyEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Utils.Extensions;

namespace Domain.Entities
{
    public class Invoice : AggregateRoot<Guid>
    {
        protected Invoice() { }

        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        [Required]
        public string InvoiceCode { get; set; }

        [Required]
        public string InvoiceNo { get; set; }

        public Guid OrderId { get; set; }

        [Required]
        public string Drawer { get; set; }

        public Guid CustomerId { get; set; }

        private decimal _amount => _invoiceItems.RoundSum(oi => oi.Price * oi.Quantity, 2);
        public decimal Amount { get => _amount; }

        public bool IsRed { get; set; }

        public bool IsValidate { get; set; } = true;

        public string Remark { get; set; }

        private readonly List<InvoiceItem> _invoiceItems = new List<InvoiceItem>();
        public IReadOnlyCollection<InvoiceItem> InvoiceItems => _invoiceItems.AsReadOnly();

        public Invoice(
            DateTime invoiceDate, 
            string invoiceCode, 
            string invoiceNo, 
            string drawer, 
            Guid orderId,
            Guid customerId, 
            bool isRed, 
            string remark, 
            List<InvoiceItem> invoiceItems)
            : this(invoiceCode, invoiceNo, drawer, orderId, customerId, isRed, remark, invoiceItems)
        {
            InvoiceDate = invoiceDate;
        }

        public Invoice(
            string invoiceCode,
            string invoiceNo,
            string drawer,
            Guid orderId,
            Guid customerId,
            bool isRed,
            string remark,
            List<InvoiceItem> invoiceItems)
        {
            InvoiceCode = invoiceCode;
            InvoiceNo = invoiceNo;
            Drawer = drawer;
            OrderId = orderId;
            CustomerId = customerId;
            IsRed = isRed;
            Remark = remark;
            _invoiceItems = invoiceItems;
        }
    }
}
