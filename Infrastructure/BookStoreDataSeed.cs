using Domain.Entities;
using MyRepositories.Repositories;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using MyRepositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure
{
    public class BookStoreDataSeed : IDataSeed
    {
        public void InitData(IServiceProvider serviceProvider)
        {
            BookStoreContext context = serviceProvider.GetService<BookStoreContext>();

            context.Database.Migrate();

            if (!context.BookTypes.Any())
            {
                context.BookTypes.AddRange(new List<BookType>
                {
                    new BookType{ TypeName = "计算机" },
                    new BookType{ TypeName = "漫画" },
                    new BookType{ TypeName = "人际关系" }
                });
            }
            if (!context.Books.Any())
            {
                context.Books.AddRange(DataSeed.Books);
            }
            if (!context.BookPrices.Any())
            {
                context.BookPrices.AddRange(DataSeed.BookPrices);
            }
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(DataSeed.Orders);
            }
            if (!context.Invoices.Any())
            {
                context.Invoices.AddRange(DataSeed.Invoices);
            }
            context.SaveChanges();
        }
    }
}
