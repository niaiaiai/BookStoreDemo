﻿using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyRepositories.Repositories;
using System.Threading.Tasks;

namespace Ids4.Middlewares.DataInitMiddlewares
{
    public class DataInitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApplicationBuilder _app;
        public DataInitMiddleware(RequestDelegate next, IApplicationBuilder app)
        {
            _next = next;
            _app = app;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (IServiceScope scope = _app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dataSeedProviders = scope.ServiceProvider.GetServices<IDataSeed>();
                foreach (var provider in dataSeedProviders)
                {
                    provider.InitData(scope.ServiceProvider);
                }
            }
            await _next(context);
        }
    }
}
