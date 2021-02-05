using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Middlewares.DataInitMiddlewares;

namespace Web.Extensions
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
