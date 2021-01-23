using Application.Dtos.Invoices;
using Domain.Entities;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IInvoiceViewService
    {
        public Task<IPageResult<GetInvoiceListOutputDto>> GetInvoiceList(GetInvoiceListInputDto dto);

        public Task<Invoice> AddInvoice(AddInvoiceDto dto);
    }
}
