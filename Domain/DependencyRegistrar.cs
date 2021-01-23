using Domain.Interfaces;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPriceService, PriceService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
