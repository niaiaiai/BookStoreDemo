using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyServices
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            string[] paths = {
                Path.Combine(AppContext.BaseDirectory, $"{nameof(Domain)}.dll"),
                Path.Combine(AppContext.BaseDirectory, $"{nameof(Application)}.dll")
            };
            List<Assembly> assemblies = new();
            foreach (string path in paths)
            {
                assemblies.Add(Assembly.LoadFile(path));
            }
            return services.AddServices(assemblies);
        }
    }
}
