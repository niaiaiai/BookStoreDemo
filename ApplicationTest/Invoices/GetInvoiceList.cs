using Application.Dtos.Invoices;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Domain.Specifications;
using Infrastructure;
using InfrastructureTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using MyRepositories.Repositories;
using MyRepositories.UnitOfWork;
using MyServices.Dtos;
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
    public class GetInvoiceList : UnitTestBase<Invoice, Guid>
    {
        private Mock<IInvoiceService> _invoiceService;
        private InvoiceViewService _invoiceViewService;
        private Mock<IRepository<Order, Guid>> _orderRepository;

        public GetInvoiceList() : base()
        {
            _invoiceService = new();
            _orderRepository = new();
            //IMapper mapper = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Invoice, GetInvoiceListOutputDto>();
            //}).CreateMapper();
            _serviceProvider.Setup(s => s.GetService(typeof(IInvoiceService))).Returns(_invoiceService.Object);
            _serviceProvider.Setup(s => s.GetService(typeof(IRepository<Order, Guid>))).Returns(_orderRepository.Object);
            //_serviceProvider.Setup(s => s.GetService(typeof(IMapper))).Returns(mapper);

            _invoiceViewService = new(_serviceProvider.Object);
        }

        [Theory]
        [MemberData(nameof(GetInvoicesByInvoiceCodeNo))]
        [MemberData(nameof(GetInvoicesByOrderId))]
        [MemberData(nameof(GetInvoicesByDrawer))]
        [MemberData(nameof(GetInvoicesByIsRed))]
        [MemberData(nameof(GetInvoicesByInvoiceDate))]
        [MemberData(nameof(GetInvoicesByAll))]
        public async Task GetInvoiceList_Should_GetList(GetInvoiceListInputDto dto, int count)
        {
            Guid? orderId = null;
            Order order = DataSeed.Orders.FirstOrDefault(o => o.OrderNo == dto.OrderNo);
            orderId = order?.Id;
            _orderRepository.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, object>>>()))
                .ReturnsAsync(order);

            InvoiceSpecification invoiceSpecification = new(dto.InvoiceCodeNo, dto.Drawer, dto.IsRed, orderId, dto.StartTime, dto.EndTime);
            var invoices = DataSeed.Invoices.AsQueryable().Where(invoiceSpecification);
            _orderRepository.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IOrderedQueryable<Order>>>(), It.IsAny<Expression<Func<Order, object>>>()))
                .ReturnsAsync(DataSeed.Orders.Where(o=> invoices.Select(i=>i.OrderId).Contains(o.Id)).AsQueryable());
            _invoiceService.Setup(s => s.GetInvoices(It.IsAny<InvoiceSpecification>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PageResult<Invoice>(invoices, dto.Index, dto.PageSize));

            var result = await _invoiceViewService.GetInvoiceList(dto);

            Assert.NotNull(result);
            Assert.Equal(count, result.Total);
            Assert.NotNull(result.Data);
            int pageCount = count - dto.PageSize * dto.Index;
            Assert.Equal(pageCount >= dto.PageSize ? dto.PageSize : (pageCount > 0 ? count : 0), result.Data.Count());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(0, -1)]
        public async Task GetInvoiceList_Should_ThrowException(int index, int pageSize)
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _invoiceViewService.GetInvoiceList(new GetInvoiceListInputDto() { Index = index, PageSize = pageSize }));
        }

        public static IEnumerable<object[]> GetInvoicesByInvoiceDate
            => new List<object[]> {
                new object[] { new GetInvoiceListInputDto { StartTime = new DateTime(2020, 12, 1), Index = 0, PageSize = 2 }, 3 }
            };
        public static IEnumerable<object[]> GetInvoicesByInvoiceCodeNo
            => new List<object[]> {
                new object[] { new GetInvoiceListInputDto { InvoiceCodeNo = "14400190112001040925", Index = 0, PageSize = 10 }, 1 }
            };
        public static IEnumerable<object[]> GetInvoicesByOrderId
            => new List<object[]> {
                new object[] { new GetInvoiceListInputDto { OrderNo = "12345666", Index = 0, PageSize = 10 }, 2 }
            };
        public static IEnumerable<object[]> GetInvoicesByDrawer
            => new List<object[]> {
                new object[] { new GetInvoiceListInputDto { Drawer = "a", Index = 2, PageSize = 2 }, 4 }
            };
        public static IEnumerable<object[]> GetInvoicesByIsRed
            => new List<object[]> {
                new object[] { new GetInvoiceListInputDto { IsRed = true, Index = 0, PageSize = 10 }, 2 }
            };
        public static IEnumerable<object[]> GetInvoicesByAll
            => new List<object[]> {
                new object[] { new GetInvoiceListInputDto { Index = 0, PageSize = 10 }, DataSeed.Invoices.Count }
            };
    }
}
