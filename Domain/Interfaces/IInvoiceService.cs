using Domain.Entities;
using Domain.Specifications;
using MyServices.Dtos;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IInvoiceService
    {
        public Task<IPageResult<Invoice>> GetInvoices(InvoiceSpecification invoiceSpecification, int index, int pageSize);

        public Task<Invoice> AddInvoice(Guid orderId, string invoiceCode, string invoiceNo, string drawer, bool isRed, string remark);
    }
}
