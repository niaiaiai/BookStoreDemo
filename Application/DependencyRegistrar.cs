using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookViewService, BookViewService>();
            services.AddScoped<IInvoiceViewService, InvoiceViewService>();
            services.AddScoped<IPriceViewService, PriceViewService>();
            services.AddScoped<IOrderViewService, OrderViewService>();
            return services;
        }
    }
}
