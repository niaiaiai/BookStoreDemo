using IdentityServer4.EntityFramework.DbContexts;
using Ids4.Middlewares.DataInitMiddlewares;
using Microsoft.AspNetCore.Builder;

namespace Ids4.Extensions
{
    public static class MiddlewaresExtensions
    {
        public static IApplicationBuilder UseDataInit(this IApplicationBuilder app)
        {
            app.UseMiddleware<DataInitMiddleware>(app);
            return app;
        }
    }
}
