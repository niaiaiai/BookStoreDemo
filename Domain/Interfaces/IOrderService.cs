using Domain.Entities;
using Domain.Specifications;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Specifications;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        public Task<IPageResult<Order>> GetOrders(OrderSpecification orderSpecification, int index, int pageSize);
        public Task<Order> OrderClose(Guid orderId);
        public Task<Order> OrderDeliver(Guid orderId);
    }
}
