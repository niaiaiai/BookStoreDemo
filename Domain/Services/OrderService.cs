using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Specifications;
using MyRepositories.Repositories;
using MyServices.Dtos;
using MyServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Specifications;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Services
{
    public class OrderService : DomainService, IOrderService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        public OrderService(IServiceProvider provider) : base(provider)
        {
            _orderRepository = provider.GetService<IRepository<Order, Guid>>();
        }

        public async Task<IPageResult<Order>> GetOrders(OrderSpecification orderSpecification, int index, int pageSize)
        {
            Guard.Against.NegativeIndexPage(pageSize, index);

            var list = await _orderRepository.GetListAsync(orderSpecification);
            return new PageResult<Order>(list, index, pageSize);
        }

        public async Task<Order> OrderClose(Guid orderId)
        {
            Order order = await _orderRepository.GetAsync(orderId);
            Guard.Against.OrderNotFound(order, orderId);
            Guard.Against.OrderStatus<OrderAlreadlyCloseException>(false, order.IsClose);

            order.IsClose = true;
            await _orderRepository.UpdateAsync(order);
            return order;
        }

        public async Task<Order> OrderDeliver(Guid orderId)
        {
            Order order = await _orderRepository.GetAsync(orderId);
            Guard.Against.OrderNotFound(order, orderId);
            Guard.Against.OrderStatus<OrderAlreadlyDeliverException>(false, order.IsDeliver);

            order.IsDeliver = true;
            await _orderRepository.UpdateAsync(order);
            return order;
        }
    }
}
