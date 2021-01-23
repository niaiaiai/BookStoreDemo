using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Infrastructure;
using InfrastructureTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using MyRepositories.Repositories;
using MyRepositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace DomainTest.Invoices
{
    public class AddInvoice : UnitTestBase<Invoice, Guid>
    {
        private InvoiceService _invoiceService;
        private Mock<IRepository<Order, Guid>> _orderRepository;

        public AddInvoice() : base()
        {
            _orderRepository = new();
            _serviceProvider.Setup(s => s.GetService(typeof(IRepository<Order, Guid>))).Returns(_orderRepository.Object);
            _invoiceService = new InvoiceService(_serviceProvider.Object);
        }

        [Theory]
        [InlineData("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8", "144001901111", "01040824", "000", false, null)]
        [InlineData("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8", "144001901111", "01040824", "000", false, "测试")]
        public async Task AddInvoice_Should_Success(Guid orderId, string invoiceCode, string invoiceNo, string drawer, bool isRed, string remark)
        {
            Order order = DataSeed.Orders.FirstOrDefault(o => o.Id == orderId);
            _repository.Setup(s => s.InsertAsync(new Invoice("", "", "", Guid.NewGuid(), Guid.NewGuid(), false, null, null)));
            _orderRepository.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, object>>>())).ReturnsAsync(order);
            Invoice invoice = await _invoiceService.AddInvoice(orderId, invoiceCode, invoiceNo, drawer, isRed, remark);

            Assert.Equal(orderId, invoice.OrderId);
            Assert.Equal(invoiceCode, invoice.InvoiceCode);
            Assert.Equal(invoiceNo, invoice.InvoiceNo);
            Assert.Equal(drawer, invoice.Drawer);
            Assert.Equal(order.CustomerId, invoice.CustomerId);
            Assert.Equal(isRed, invoice.IsRed);
            Assert.Equal(remark, invoice.Remark);
            Assert.True(invoice.IsValidate);
            Assert.Equal(order.OrderItems.Count, invoice.InvoiceItems.Count);
            foreach (OrderItem orderItem in order.OrderItems)
            {
                InvoiceItem invoiceItem = invoice.InvoiceItems.FirstOrDefault(ii => ii.BookId == orderItem.BookId);
                Assert.NotNull(invoiceItem);
                Assert.Equal(orderItem.Price, invoiceItem.Price);
                Assert.Equal(orderItem.Quantity, invoiceItem.Quantity);
                Assert.Equal(invoice.Id, invoiceItem.InvoiceId);
            }
        }
    }
}
