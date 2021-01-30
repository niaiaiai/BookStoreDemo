using System;

namespace Ids4.Exceptions
{
    public class CreateUserException : Exception
    {
        public CreateUserException(string message) : base(message) { }

        public CreateUserException() : base() { }
    }
}
