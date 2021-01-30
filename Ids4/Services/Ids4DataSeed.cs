using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Ids4.Middlewares.DataInitMiddlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRepositories.Repositories;
using System;
using System.Linq;

namespace Ids4.Services
{
    public class Ids4DataSeed<ConfDbContext, OpsDbContext> : IDataSeed
        where ConfDbContext : ConfigurationDbContext<ConfDbContext>
        where OpsDbContext : PersistedGrantDbContext<OpsDbContext>
    {
        public void InitData(IServiceProvider serviceProvider)
        {
            var configContext = serviceProvider.GetRequiredService<ConfDbContext>();
            var opsContext = serviceProvider.GetRequiredService<OpsDbContext>();

            opsContext.Database.Migrate();
            configContext.Database.Migrate();

            if (!configContext.Clients.Any())
            {
                foreach (var client in Ids4Config.Clients)
                {
                    configContext.Clients.Add(client.ToEntity());
                }
            }
            if (!configContext.IdentityResources.Any())
            {
                foreach (var resource in Ids4Config.IdentityResources)
                {
                    configContext.IdentityResources.Add(resource.ToEntity());
                }
            }
            if (!configContext.ApiScopes.Any())
            {
                foreach (var scope in Ids4Config.ApiScopes)
                {
                    configContext.ApiScopes.Add(scope.ToEntity());
                }
            }
            if (!configContext.ApiResources.Any())
            {
                foreach (var resource in Ids4Config.ApisResource)
                {
                    configContext.ApiResources.Add(resource.ToEntity());
                }
            }
            configContext.SaveChanges();
        }
    }
}
