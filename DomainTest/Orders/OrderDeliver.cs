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
using System.Text;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace DomainTest.Orders
{
    public class OrderDeliver : UnitTestBase<Order, Guid>
    {
        private OrderService _orderService;

        public OrderDeliver() : base()
        {
            _orderService = new OrderService(_serviceProvider.Object);
        }

        [Theory]
        [InlineData("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8")]
        public async Task OrderDeliver_Should_Success(Guid orderId)
        {
            _repository.Setup(s => s.GetAsync(orderId)).ReturnsAsync(DataSeed.Orders.FirstOrDefault(o => o.Id == orderId));
            _repository.Setup(s => s.UpdateAsync(new Order("", Guid.NewGuid(), false, null)));

            Order order = await _orderService.OrderDeliver(orderId);
            Assert.Equal(orderId, order.Id);
            Assert.True(order.IsDeliver);
        }

        [Theory]
        [InlineData("bbbb4ed8-3e36-40fd-b57e-dfbbcf7e1bb8")]
        public async Task OrderDeliver_Should_ThrowOrderNotFoundException(Guid orderId)
        {
            _repository.Setup(s => s.GetAsync(orderId)).ReturnsAsync(DataSeed.Orders.FirstOrDefault(o => o.Id == orderId));
            await Assert.ThrowsAsync<OrderNotFoundException>(() => _orderService.OrderDeliver(orderId));
        }

        [Theory]
        [InlineData("6a7a6f4b-3df2-4018-b37a-4ae7b6272222")]
        public async Task OrderDeliver_Should_ThrowAlreadlyDeliverException(Guid orderId)
        {
            _repository.Setup(s => s.GetAsync(orderId)).ReturnsAsync(DataSeed.Orders.FirstOrDefault(o => o.Id == orderId));
            await Assert.ThrowsAsync<OrderAlreadlyDeliverException>(() => _orderService.OrderDeliver(orderId));
        }
    }
}
