using Ardalis.GuardClauses;
using MyEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }
        public Guid OrderId { get; set; }

        public Guid BookId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public void AddQuantity(int quantity)
        {
            Guard.Against.NegativeQuantity(quantity);
            Quantity += quantity;
        }

        public OrderItem(Guid orderId, Guid bookId, decimal price, int quantity)
        {
            Guard.Against.NegativeQuantity(quantity);
            Guard.Against.NegativePrice(price);
            OrderId = orderId;
            BookId = bookId;
            Quantity = quantity;
            Price = price;
        }
    }
}
