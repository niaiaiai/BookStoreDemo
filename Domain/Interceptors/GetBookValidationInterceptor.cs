using Castle.DynamicProxy;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interceptors
{
    public class GetBookValidationInterceptor : IInterceptor
    {
        //private IBookService _bookService;

        //public GetBookValidationInterceptor(IBookService bookService)
        //{
        //    _bookService = bookService;
        //}

        public void Intercept(IInvocation invocation)
        {
            throw new ArgumentOutOfRangeException();
            var args = invocation.Arguments;
            //invocation.Method.Invoke(_bookService, invocation.Arguments);
        }
    }
}
