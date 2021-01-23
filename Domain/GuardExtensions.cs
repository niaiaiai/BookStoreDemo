using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses
{
    public static class BookGuards
    {
        public static void NegativeIndexPage(this IGuardClause guardClause, int pageSize, int index)
        {
            if (pageSize <= 0 || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public static void NegativePrice(this IGuardClause guardClause, decimal price)
        {
            if (price < 0)
            {
                throw new BookPriceOutOfRangeException($"单价{price}小于0");
            }
        }

        public static void NegativeQuantity(this IGuardClause guardClause, int quantity)
        {
            if (quantity <= 0)
            {
                throw new QuantityOutOfRangeException($"数量{quantity}小于0");
            }
        }

        public static void BookNotFound(this IGuardClause guardClause, Book book, Guid bookId)
        {
            if (book == null)
            {
                throw new BookNotFoundException(bookId);
            }
        }

        public static void BookNotFound(this IGuardClause guardClause, Book book, string isbn)
        {
            if (book == null)
            {
                throw new BookNotFoundException($"找不到书号{isbn}的图书");
            }
        }

        public static void BookPriceNotFound(this IGuardClause guardClause, BookPrice bookPrice, Guid bookPriceId)
        {
            if (bookPrice == null)
            {
                throw new BookPriceNotFoundException(bookPriceId);
            }
        }

        public static void OrderNotFound(this IGuardClause guardClause, Order order, Guid orderId)
        {
            if (order == null)
            {
                throw new OrderNotFoundException(orderId);
            }
        }
        public static void OrderNotFound(this IGuardClause guardClause, Order order, string orderNo)
        {
            if (order == null)
            {
                throw new OrderNotFoundException($"找不到订单号{orderNo}的订单");
            }
        }

        public static void OrderStatus<E>(this IGuardClause guardClause, bool expect, bool actual) where E : Exception
        {
            if (expect != actual)
            {
                throw Activator.CreateInstance<E>();
            }
        }
    }
}
