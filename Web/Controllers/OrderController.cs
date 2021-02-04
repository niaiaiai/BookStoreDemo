using Application.Dtos.Orders;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderViewService _orderViewService;
        private readonly IOrderService _orderService;
        public OrderController(IOrderViewService orderViewService, IOrderService orderService)
        {
            _orderViewService = orderViewService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IPageResult<GetOrderListOutputDto>>> GetOrderList([FromQuery] GetOrderListInputDto dto)
        {
            var result = await _orderViewService.GetOrderList(dto);
            return Ok(result);
        }

        [HttpPost("close/{orderId}")]
        public async Task<ActionResult> OrderClose(Guid orderId)
        {
            await _orderService.OrderClose(orderId);
            return Ok();
        }

        [HttpPost("deliver/{orderId}")]
        public async Task<ActionResult> OrderDeliver(Guid orderId)
        {
            await _orderService.OrderDeliver(orderId);
            return Ok();
        }
    }
}
