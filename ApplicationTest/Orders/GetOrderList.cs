using Application.Dtos.Orders;
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
using MyRepositories.UnitOfWork;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace ApplicationTest.Orders
{
    public class GetOrderList : UnitTestBase<Order, Guid>
    {
        private Mock<IOrderService> _orderService;
        private OrderViewService _orderViewService;
        public GetOrderList() : base()
        {
            _orderService = new();
            IMapper mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, GetOrderListOutputDto>();
            }).CreateMapper();
            _serviceProvider.Setup(s => s.GetService(typeof(IOrderService))).Returns(_orderService.Object);
            _serviceProvider.Setup(s => s.GetService(typeof(IMapper))).Returns(mapper);

            _orderViewService = new(_serviceProvider.Object);
        }

        [Theory]
        [MemberData(nameof(GetOrdersByOrderNo))]
        [MemberData(nameof(GetOrdersByOrderDate))]
        [MemberData(nameof(GetOrdersByNeedInvoice))]
        [MemberData(nameof(GetOrdersByDelivery))]
        [MemberData(nameof(GetOrdersByClose))]
        [MemberData(nameof(GetOrdersByDone))]
        [MemberData(nameof(GetOrdersByAll))]
        [MemberData(nameof(GetOrdersByAny))]
        public async Task GetOrderList_Should_GetList(GetOrderListInputDto dto, int count)
        {
            OrderSpecification orderSpecification = new(dto.OrderNo, dto.IsClose, dto.IsDeliver, dto.IsDone, dto.NeedInvoice, dto.StartTime, dto.EndTime);
            var orders = DataSeed.Orders.AsQueryable().Where(orderSpecification);
            _orderService.Setup(s => s.GetOrders(It.IsAny<OrderSpecification>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PageResult<Order>(orders, dto.Index, dto.PageSize));
            var result = await _orderViewService.GetOrderList(dto);

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
        public async Task GetOrders_Should_ThrowException(int index, int pageSize)
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _orderViewService.GetOrderList(new GetOrderListInputDto { Index = index, PageSize = pageSize }));
        }

        public static IEnumerable<object[]> GetOrdersByOrderNo
            => new List<object[]> {
                new object[] { new GetOrderListInputDto { OrderNo = "12345678", Index = 0, PageSize = 10 }, 1 }
            };
        public static IEnumerable<object[]> GetOrdersByOrderDate
            => new List<object[]> {
                new object[] { new GetOrderListInputDto { StartTime = new DateTime(2020, 12, 27), EndTime = new DateTime(2021, 1, 1), Index = 0, PageSize = 10 }, 3 }
            };
        public static IEnumerable<object[]> GetOrdersByNeedInvoice
            => new List<object[]> {
                new object[] { new GetOrderListInputDto { NeedInvoice = true, Index = 0, PageSize = 10 }, 2 }
            };
        public static IEnumerable<object[]> GetOrdersByDelivery
            => new List<object[]> {
                new object[] { new GetOrderListInputDto { IsDeliver = true, Index = 0, PageSize = 2 }, 2 }
            };
        public static IEnumerable<object[]> GetOrdersByClose
            => new List<object[]> {
                new object[] { new GetOrderListInputDto { IsClose = false, EndTime = new DateTime(2099, 12, 12), Index = 0, PageSize = 2 }, 3 }
            };
        public static IEnumerable<object[]> GetOrdersByDone
            => new List<object[]> {
                new object[] { new GetOrderListInputDto { IsDone = false, Index = 1, PageSize = 1 }, 2 }
            };
        public static IEnumerable<object[]> GetOrdersByAll
            => new List<object[]> {
                new object[] { new GetOrderListInputDto { EndTime = new DateTime(2099, 12, 12), Index = 0, PageSize = 10 }, DataSeed.Orders.Count }
            };
        public static IEnumerable<object[]> GetOrdersByAny
            => new List<object[]> {
                new object[] { new GetOrderListInputDto { IsDeliver = true, IsDone = true, Index = 0, PageSize = 10 }, 1 }
            };
    }
}
