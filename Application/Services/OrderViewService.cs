using Application.Dtos.Orders;
using Application.Interfaces;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Interfaces;
using Domain.Specifications;
using MyServices.Dtos;
using MyServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class OrderViewService : ApplicationService, IOrderViewService
    {
        private readonly IOrderService _orderService;
        public OrderViewService(IServiceProvider provider) : base(provider)
        {
            _orderService = provider.GetService<IOrderService>();
        }

        public async Task<IPageResult<GetOrderListOutputDto>> GetOrderList(GetOrderListInputDto dto)
        {
            Guard.Against.NegativeIndexPage(dto.PageSize, dto.Index);
            OrderSpecification orderSpecification = new(dto.OrderNo, dto.IsClose, dto.IsDeliver, dto.IsDone, dto.NeedInvoice, dto.StartTime, dto.EndTime);
            var orders = await _orderService.GetOrders(orderSpecification, dto.Index, dto.PageSize);
            return new PageResult<GetOrderListOutputDto> { Total = orders.Total, Data = _mapper.Map<IEnumerable<GetOrderListOutputDto>>(orders.Data) };
        }

        //public async Task OrderClose(Guid orderId)
        //{
        //    await _orderService.OrderClose(orderId);
        //}

        //public async Task OrderDeliver(Guid orderId)
        //{
        //    await _orderService.OrderDeliver(orderId);
        //}
    }
}
