//using Application.Services;
//using AutoMapper;
//using Domain.Entities;
//using Domain.Interfaces;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TestBase;
//using Xunit;

//namespace ApplicationTest.Orders
//{
//    public class UpdateOrder : UnitTestBase<Order, Guid>
//    {
//        private readonly Mock<IOrderService> _orderServiceMock;
//        private readonly OrderViewService _orderViewService;
//        public UpdateOrder() : base()
//        {
//            _orderServiceMock = new();
//            _serviceProvider.Setup(s => s.GetService(typeof(IOrderService))).Returns(_orderServiceMock.Object);
//            _orderViewService = new(_serviceProvider.Object);
//        }

//        [Theory]
//        [InlineData("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8")]
//        public async Task OrderClose_Should_Success(Guid orderId)
//        {
//            _orderServiceMock.Setup(s => s.OrderClose(Guid.NewGuid())).ReturnsAsync(new Order("", Guid.NewGuid(), false, null));
//            await _orderViewService.OrderClose(orderId);
//        }

//        [Theory]
//        [InlineData("e8284ed8-3e36-40fd-b57e-dfbbcf7e1bb8")]
//        public async Task OrderDeliver_Should_Success(Guid orderId)
//        {
//            _orderServiceMock.Setup(s => s.OrderDeliver(Guid.NewGuid())).ReturnsAsync(new Order("", Guid.NewGuid(), false, null));
//            await _orderViewService.OrderDeliver(orderId);
//        }
//    }
//}
