using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {

        public BookStoreContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();                
            switch(args[1])
            {
                case "Development":
                    configurationBuilder.AddJsonFile($"appsettings.{args[1]}.json", true, true);
                    break;
                case "Production":
                    configurationBuilder.AddJsonFile("appsettings.json", true, true);
                    break;
            }
            var configuration = configurationBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<BookStoreContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookStoreConnection"));

            return new BookStoreContext(optionsBuilder.Options);
        }
    }
}
