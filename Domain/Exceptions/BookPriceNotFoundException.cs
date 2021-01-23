using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class BookPriceNotFoundException : Exception
    {
        public BookPriceNotFoundException(Guid bookPriceId) : base($"No book price found with id {bookPriceId}")
        {
        }

        public BookPriceNotFoundException(string message) : base(message)
        {
        }

        public BookPriceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
