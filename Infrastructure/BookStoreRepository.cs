using MyRepositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BookStoreRepository<Entity> : Repository<BookStoreContext, Entity> where Entity : class
    {
        public BookStoreRepository(BookStoreContext bookStoreContext) : base(bookStoreContext) { }
    }

    public class BookStoreRepository<Entity, Key> : Repository<BookStoreContext, Entity, Key> where Entity : class
    {
        public BookStoreRepository(BookStoreContext bookStoreContext) : base(bookStoreContext) { }
    }
}
