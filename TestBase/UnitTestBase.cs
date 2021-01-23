using Infrastructure;
using InfrastructureTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using MyRepositories.Repositories;
using MyRepositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBase
{
    public class UnitTestBase<Entity, Key> where Entity : class
    {
        protected Mock<IServiceProvider> _serviceProvider;
        protected readonly IUnitOfWork _unitOfWork;
        protected DbContext _context;
        protected Mock<IRepository<Entity, Key>> _repository;

        public UnitTestBase()
        {
            _serviceProvider = new();
            _context = DbConnect.dbContext;
            _unitOfWork = new UnitOfWork<BookStoreContext>((BookStoreContext)_context, Options.Create(new UnitOfWorkOptions()));
            _repository = new();

            _serviceProvider.Setup(s => s.GetService(typeof(IUnitOfWork))).Returns(_unitOfWork);
            _serviceProvider.Setup(s => s.GetService(typeof(IRepository<Entity, Key>))).Returns(_repository.Object);
        }
    }
}
