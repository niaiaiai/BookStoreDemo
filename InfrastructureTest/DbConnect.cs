using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace InfrastructureTest
{
    public class DbConnect
    {
        public static readonly DbContext dbContext;
        static DbConnect()
        {
            dbContext = DbConfig();
        }


        private static DbContext DbConfig()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", true, true)
                .Build();

            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseSqlServer(configuration.GetConnectionString("BookStoreConnection"))
                .Options;

            DbContext context = new BookStoreContext(options);

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //DataSeed.InitBookStoreData((BookStoreContext)context).Wait();
            return context;
        }
    }
}
