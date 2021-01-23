using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class OrderNotNeedInvoiceException : Exception
    {
        public OrderNotNeedInvoiceException() : base() { }

        public OrderNotNeedInvoiceException(string message) : base(message)
        {
        }

        public OrderNotNeedInvoiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
