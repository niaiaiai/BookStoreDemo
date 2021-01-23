using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class QuantityOutOfRangeException : Exception
    {
        public QuantityOutOfRangeException(string message) : base(message) { }

        public QuantityOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
