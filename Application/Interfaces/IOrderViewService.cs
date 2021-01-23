using Application.Dtos.Orders;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderViewService
    {
        public Task<IPageResult<GetOrderListOutputDto>> GetOrderList(GetOrderListInputDto bookDto);
        //public Task OrderClose(Guid orderId);
        //public Task OrderDeliver(Guid orderId);
    }
}
