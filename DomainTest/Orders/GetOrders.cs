using Domain.Entities;
using Domain.Services;
using Domain.Specifications;
using Infrastructure;
using Moq;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestBase;
using Volo.Abp.Specifications;
using Xunit;

namespace DomainTest.Orders
{
    public class GetOrders : UnitTestBase<Order, Guid>
    {
        private OrderService _orderService;

        public GetOrders() : base()
        {
            _orderService = new OrderService(_serviceProvider.Object);

        }

        [Theory]
        [MemberData(nameof(GetOrdersByOrderNo))]
        [MemberData(nameof(GetOrdersByOrderDate))]
        //[MemberData(nameof(GetOrdersByCustomer))]
        [MemberData(nameof(GetOrdersByNeedInvoice))]
        [MemberData(nameof(GetOrdersByDelivery))]
        [MemberData(nameof(GetOrdersByClose))]
        [MemberData(nameof(GetOrdersByDone))]
        [MemberData(nameof(GetOrdersByAll))]
        [MemberData(nameof(GetOrdersByAny))]
        public async Task GetOrders_ShouldGet_Orders(OrderSpecification orderSpecification, int index, int pageSize, int count)
        {
            _repository.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Func<IQueryable<Order>, IOrderedQueryable<Order>>>(), It.IsAny<Expression<Func<Order, object>>>()))
                .ReturnsAsync(DataSeed.Orders.AsQueryable().Where(orderSpecification));
            IPageResult<Order> orders = await _orderService.GetOrders(orderSpecification, index, pageSize);

            Assert.NotNull(orders);
            Assert.Equal(count, orders.Total);
            int pageCount = count - pageSize * index;
            Assert.Equal(pageCount >= pageSize ? pageSize : (pageCount > 0 ? count : 0), orders.Data.Count());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(0, -1)]
        public async Task GetOrders_Should_ThrowException(int index, int pageSize)
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _orderService.GetOrders(new OrderSpecification(), index, pageSize));
        }

        public static IEnumerable<object[]> GetOrdersByOrderNo
            => new List<object[]> {
                new object[] { new OrderSpecification("12345678"), 0, 10, 1 }
            };
        public static IEnumerable<object[]> GetOrdersByOrderDate
            => new List<object[]> {
                new object[] { new OrderSpecification(null, null, null, null, null, new DateTime(2020, 12, 27), new DateTime(2021, 1, 1)), 0, 10, 3 }
            };
        //public static IEnumerable<object[]> GetOrdersByCustomer
        //    => new List<object[]> {
        //        new object[] { new OrderSpecification("设计模式", "9787111618331"), 0, 10, 2 }
        //    };
        public static IEnumerable<object[]> GetOrdersByNeedInvoice
            => new List<object[]> {
                new object[] { new OrderSpecification(null, null, null, null, true), 0, 10, 2 }
            };
        public static IEnumerable<object[]> GetOrdersByDelivery
            => new List<object[]> {
                new object[] { new OrderSpecification(null, null, true), 0, 2, 2 }
            };
        public static IEnumerable<object[]> GetOrdersByClose
            => new List<object[]> {
                new object[] { new OrderSpecification(null, false), 0, 2, 3 }
            };
        public static IEnumerable<object[]> GetOrdersByDone
            => new List<object[]> {
                new object[] { new OrderSpecification(null, null, null, false), 1, 1, 2 }
            };
        public static IEnumerable<object[]> GetOrdersByAll
            => new List<object[]> {
                new object[] { new OrderSpecification(), 0, 10, DataSeed.Orders.Count }
            };
        public static IEnumerable<object[]> GetOrdersByAny
            => new List<object[]> {
                new object[] { new OrderSpecification(null, null, true, true), 0, 10, 1 }
            };
    }
}
