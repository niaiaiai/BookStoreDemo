using Application.Dtos.Invoices;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure;
using Moq;
using MyRepositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace ApplicationTest.Invoices
{
    public class AddInvoice : UnitTestBase<Invoice, Guid>
    {
        private readonly Mock<IInvoiceService> _invoiceServiceMock;
        private readonly InvoiceViewService _invoiceViewService;
        private readonly Mock<IRepository<Order, Guid>> _orderRepository;
        public AddInvoice() : base()
        {
            _invoiceServiceMock = new();
            _orderRepository = new();
            _serviceProvider.Setup(s => s.GetService(typeof(IInvoiceService))).Returns(_invoiceServiceMock.Object);
            _serviceProvider.Setup(s => s.GetService(typeof(IRepository<Order, Guid>))).Returns(_orderRepository.Object);
            _invoiceViewService = new(_serviceProvider.Object);
        }

        [Theory]
        [MemberData(nameof(AddInvoice1))]
        [MemberData(nameof(AddInvoice2))]
        public async Task AddInvoice_Should_Success(AddInvoiceDto dto)
        {
            _invoiceServiceMock.Setup(s => s.AddInvoice(Guid.NewGuid(), "", "", "", false, ""))
                .ReturnsAsync(new Invoice("", "", "", Guid.NewGuid(), Guid.NewGuid(), false, "", new List<InvoiceItem> { }));
            _orderRepository.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, object>>>()))
                .ReturnsAsync(new Order("", Guid.NewGuid(), true, null));
            await _invoiceViewService.AddInvoice(dto);
        }


        [Theory]
        [InlineData("000")]
        public async Task AddInvoice_Should_ThrowOrderNotFound(string orderNo)
        {
            Order order = DataSeed.Orders.FirstOrDefault(o => o.OrderNo == orderNo);
            _orderRepository.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, object>>>()))
                .ReturnsAsync(order);
            await Assert.ThrowsAnyAsync<OrderNotFoundException>(() => _invoiceViewService.AddInvoice(new AddInvoiceDto { OrderNo = orderNo }));
        }

        [Theory]
        [InlineData("12345666")]
        public async Task AddInvoice_Should_ThrowOrderNotNeedInvoice(string orderNo)
        {
            Order order = DataSeed.Orders.FirstOrDefault(o => o.OrderNo == orderNo);
            _orderRepository.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, object>>>()))
                .ReturnsAsync(order);
            await Assert.ThrowsAnyAsync<OrderNotNeedInvoiceException>(() => _invoiceViewService.AddInvoice(new AddInvoiceDto { OrderNo = orderNo }));
        }

        public static IEnumerable<object[]> AddInvoice1
            => new List<object[]> {
                    new object[] { new AddInvoiceDto() { 
                        OrderNo = "12345665", 
                        InvoiceCode = "144001901111", 
                        InvoiceNo = "01040824",
                        Drawer = "000",
                        IsRed = false,
                        Remark = null
                    } }
            };
        public static IEnumerable<object[]> AddInvoice2
            => new List<object[]> {
                    new object[] { new AddInvoiceDto() { 
                        OrderNo = "12345665", 
                        InvoiceCode = "144001901111", 
                        InvoiceNo = "01040824",
                        Drawer = "000",
                        IsRed = false,
                        Remark = "aaa"
                    } }
            };
    }
}
