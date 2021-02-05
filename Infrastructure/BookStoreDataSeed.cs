using Domain.Entities;
using MyRepositories.Repositories;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using MyRepositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookStoreDataSeed : IDataSeed
    {
        public void InitData(IServiceProvider serviceProvider)
        {
            BookStoreContext context = serviceProvider.GetService<BookStoreContext>();

            context.Database.Migrate();

            context.BookTypes.AddRange(new List<BookType>
            {
                new BookType{ TypeName = "计算机" },
                new BookType{ TypeName = "漫画" },
                new BookType{ TypeName = "人际关系" }
            });
            context.Books.AddRange(DataSeed.Books);
            context.BookPrices.AddRange(DataSeed.BookPrices);
            context.Orders.AddRange(DataSeed.Orders);
            context.Invoices.AddRange(DataSeed.Invoices);
            context.SaveChanges();
        }
    }
}
