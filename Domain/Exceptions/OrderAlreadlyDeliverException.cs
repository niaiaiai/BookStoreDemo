using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class OrderAlreadlyDeliverException : Exception
    {
        public OrderAlreadlyDeliverException() : base()
        {
        }

        public OrderAlreadlyDeliverException(string message) : base(message)
        {
        }

        public OrderAlreadlyDeliverException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
