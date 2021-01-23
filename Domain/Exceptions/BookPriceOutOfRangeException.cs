using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class BookPriceOutOfRangeException : Exception
    {
        public BookPriceOutOfRangeException(string message) : base(message)
        {
        }

        public BookPriceOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
