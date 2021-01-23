using MyEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class InvoiceItem : Entity
    {
        protected InvoiceItem() { }
        public Guid InvoiceId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public InvoiceItem(Guid invoiceId, Guid bookId, int quantity, decimal price)
        {
            InvoiceId = invoiceId;
            BookId = bookId;
            Quantity = quantity;
            Price = price;
        }
    }
}
