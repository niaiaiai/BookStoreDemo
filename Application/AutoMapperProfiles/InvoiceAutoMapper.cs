using Application.Dtos.Invoices;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapperProfiles
{
    public class InvoiceAutoMapper : Profile
    {
        public InvoiceAutoMapper()
        {
            //CreateMap<Invoice, GetInvoiceListOutputDto>();
        }
    }
}
