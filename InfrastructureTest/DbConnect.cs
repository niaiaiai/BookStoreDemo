using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
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
            string connectionString = @"Server=(LocalDb)\MSSQLLocalDB;Database=BookStore;Trusted_Connection=True;MultipleActiveResultSets=true";

            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            DbContext context = new BookStoreContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            DataSeed.InitBookStoreData((BookStoreContext)context).Wait();
            return context;
        }
    }
}
