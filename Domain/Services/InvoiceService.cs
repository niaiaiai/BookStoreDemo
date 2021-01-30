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
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Services
{
    public class InvoiceService : DomainService, IInvoiceService
    {
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly IRepository<Order, Guid> _orderRepository;
        public InvoiceService(IServiceProvider provider) : base(provider)
        {
            _invoiceRepository = provider.GetService<IRepository<Invoice, Guid>>();
            _orderRepository = provider.GetService<IRepository<Order, Guid>>();
        }

        public async Task<Invoice> AddInvoice(Guid orderId, string invoiceCode, string invoiceNo, string drawer, bool isRed, string remark)
        {
            Order order = await _orderRepository.GetAsync(o => o.Id == orderId, o => o.OrderItems);

            Guid invoiceId = Guid.NewGuid();
            List<InvoiceItem> invoiceItems = new List<InvoiceItem>();
            foreach (OrderItem orderItem in order.OrderItems)
            {
                invoiceItems.Add(new InvoiceItem(invoiceId, orderItem.BookId, orderItem.Quantity, orderItem.Price));
            }
            Invoice invoice = new Invoice(invoiceCode, invoiceNo, drawer, orderId, order.CustomerId, isRed, remark, invoiceItems) { Id = invoiceId };
            await _invoiceRepository.InsertAsync(invoice);

            return invoice;
        }

        public async Task<IPageResult<Invoice>> GetInvoices(InvoiceSpecification invoiceSpecification, int index, int pageSize)
        {
            var list = await _invoiceRepository.GetListAsync(invoiceSpecification, null, i => i.InvoiceItems);
            return new PageResult<Invoice>(list, index, pageSize);
        }
    }
}
