using Domain.Interfaces;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using MyCore.DependencyInjection;
using MyRepositories.Repositories;

namespace Domain
{
    public class DependencyInjection : DefaultConfigureServices
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPriceService, PriceService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
