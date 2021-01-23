using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class OrderAlreadlyCloseException : Exception
    {
        public OrderAlreadlyCloseException() : base()
        {
        }

        public OrderAlreadlyCloseException(string message) : base(message)
        {
        }

        public OrderAlreadlyCloseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
