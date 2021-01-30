using Application.Dtos.Invoices;
using Application.Interfaces;
using Ardalis.GuardClauses;
using AutoMapper;
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
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class InvoiceViewService : ApplicationService, IInvoiceViewService
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IRepository<Order, Guid> _orderRepository;
        public InvoiceViewService(IServiceProvider provider) : base(provider)
        {
            _invoiceService = provider.GetService<IInvoiceService>();
            _orderRepository = provider.GetService<IRepository<Order, Guid>>();
        }
        public async Task<IPageResult<GetInvoiceListOutputDto>> GetInvoiceList(GetInvoiceListInputDto dto)
        {
            Guard.Against.NegativeIndexPage(dto.PageSize, dto.Index);
            Guid? orderId = null;
            if (!string.IsNullOrWhiteSpace(dto.OrderNo))
            {
                Order order = await _orderRepository.GetAsync(o => o.OrderNo == dto.OrderNo.Trim());
                orderId = order.Id;
            }
            
            InvoiceSpecification invoiceSpecification = new(dto.InvoiceCodeNo, dto.Drawer, dto.IsRed, orderId, dto.StartTime, dto.EndTime);
            var list = await _invoiceService.GetInvoices(invoiceSpecification, dto.Index, dto.PageSize);
            var orders = await _orderRepository.GetListAsync(o => list.Data.Select(i => i.OrderId).Contains(o.Id));
            IEnumerable<GetInvoiceListOutputDto> resultData = list.Data.Join(orders.ToList(), i => i.OrderId, o => o.Id, (i, o) => new GetInvoiceListOutputDto
            {
                Id = i.Id,
                Drawer = i.Drawer,
                InvoiceCode = i.InvoiceCode,
                InvoiceDate = i.InvoiceDate,
                InvoiceNo = i.InvoiceNo,
                IsRed = i.IsRed,
                OrderNo = o.OrderNo,
                Remark = i.Remark,
                Amount = i.Amount
            });
            return new PageResult<GetInvoiceListOutputDto> { Total = list.Total, Data = resultData };
        }

        public async Task<Invoice> AddInvoice(AddInvoiceDto dto)
        {
            Order order = await _orderRepository.GetAsync(o => o.OrderNo == dto.OrderNo.Trim());
            Guard.Against.OrderNotFound(order, dto.OrderNo);
            Guard.Against.OrderStatus<OrderNotNeedInvoiceException>(true, order.NeedInvoice);

            return await _invoiceService.AddInvoice(order.Id, dto.InvoiceCode, dto.InvoiceNo, dto.Drawer, dto.IsRed, dto.Remark);
        }
    }
}
