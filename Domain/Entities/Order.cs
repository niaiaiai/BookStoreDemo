using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Utils.Extensions;

namespace Domain.Entities
{
    public class Order : AggregateRoot<Guid>
    {
        protected Order() { }

        [Required]
        public string OrderNo { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public Guid CustomerId { get; set; }

        public bool NeedInvoice { get; set; }

        public bool IsDeliver { get; set; } = false;

        public bool IsClose { get; set; } = false;

        public bool IsDone { get; set; } = false;

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private decimal _amount => _orderItems.RoundSum(oi => oi.Price * oi.Quantity, 2);
        public decimal Amount { get => _amount; }

        public Order(
            string orderNo, 
            Guid customerId, 
            bool needInvoice, 
            bool isDeliver,
            bool isClose, 
            bool isDone, 
            DateTime orderDate, 
            List<OrderItem> orderItems)
            : this(orderNo, customerId, needInvoice, orderItems)
        {
            IsDeliver = isDeliver;
            IsClose = isClose;
            IsDone = isDone;
            OrderDate = orderDate;
        }

        public Order(string orderNo, Guid customerId, bool needInvoice, List<OrderItem> orderItems)
        {
            OrderNo = orderNo;
            CustomerId = customerId;
            NeedInvoice = needInvoice;
            _orderItems = orderItems;
        }

        public void AddOrderItem(Guid orderId, Guid bookId, decimal price, int quantity = 1)
        {
            var existingOrderForProduct = _orderItems.Where(oi => oi.BookId == bookId)
                .SingleOrDefault();

            if (existingOrderForProduct != null)
            {
                existingOrderForProduct.AddQuantity(quantity);
            }
            else
            {
                //add validated new order item
                var orderItem = new OrderItem(orderId, bookId, price, quantity);
                _orderItems.Add(orderItem);
            }
        }
    }
}
