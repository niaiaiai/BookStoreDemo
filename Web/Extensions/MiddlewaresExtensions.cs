using Microsoft.AspNetCore.Builder;
//using MyWeb.Middlewares.DataInitMiddlewares;

namespace Web.Extensions
{
    public static class MiddlewaresExtensions
    {
        public static IApplicationBuilder UseDataInit(this IApplicationBuilder app)
        {
            //app.UseMiddleware<DataInitMiddleware>(app);
            return app;
        }
    }
}
