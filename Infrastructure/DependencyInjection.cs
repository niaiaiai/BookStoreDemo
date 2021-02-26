using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCore.DependencyInjection;
using MyRepositories;
using MyRepositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DependencyInjection : DefaultConfigureServices
    {
        private readonly IConfiguration _configuration;
        public DependencyInjection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<BookStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("BookStoreConnection")));

            services.AddScoped(typeof(IReadOnlyRepository<>), typeof(BookStoreRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(BookStoreRepository<>));
            services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(BookStoreRepository<,>));
            services.AddScoped(typeof(IRepository<,>), typeof(BookStoreRepository<,>));

            services.AddUnitOfWork<BookStoreContext>();

            services.AddScoped<IDataSeed, BookStoreDataSeed>();
            return services;
        }
    }
}
