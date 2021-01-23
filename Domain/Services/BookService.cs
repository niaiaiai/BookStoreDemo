
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using MyRepositories.Repositories;
using MyServices.Dtos;
using MyServices.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class BookService : DomainService, IBookService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        public BookService(IServiceProvider provider) : base(provider)
        {
            _bookRepository = provider.GetService<IRepository<Book, Guid>>();
        }

        public async Task<Book> EditImage(Guid bookId, string imageBase64)
        {
            Book book = await _bookRepository.GetAsync(bookId);
            Guard.Against.BookNotFound(book, bookId);

            book.Image = imageBase64;
            await _bookRepository.UpdateAsync(book);
            return book;
        }

        public async Task<Book> EditRemark(Guid bookId, string remark)
        {
            Book book = await _bookRepository.GetAsync(bookId);
            Guard.Against.BookNotFound(book, bookId);

            book.Remark = remark;
            await _bookRepository.UpdateAsync(book);
            return book;
        }

        public async Task<IPageResult<Book>> GetBooks(BookSpecification specification, int index, int pageSize)
        {
            Guard.Against.NegativeIndexPage(pageSize, index);
            var list = await _bookRepository.GetListAsync(specification);
            return new PageResult<Book>(list, index, pageSize);
        }
    }
}
