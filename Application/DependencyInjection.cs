using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyCore.DependencyInjection;
using System;

namespace Application
{
    public class DependencyInjection : DefaultConfigureServices
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBookViewService, BookViewService>();
            services.AddScoped<IInvoiceViewService, InvoiceViewService>();
            services.AddScoped<IPriceViewService, PriceViewService>();
            services.AddScoped<IOrderViewService, OrderViewService>();
            return services;
        }
    }
}
