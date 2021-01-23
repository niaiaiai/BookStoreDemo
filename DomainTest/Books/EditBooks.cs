using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure;
using Moq;
using MyRepositories.Repositories;
using MyRepositories.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace DomainTest.Books
{
    public class EditBooks : UnitTestBase<Book, Guid>
    {
        private BookService _bookService;

        public EditBooks() : base()
        {
            _bookService = new BookService(_serviceProvider.Object);
            _repository.Setup(s => s.UpdateAsync(new Book("", "", "", "", "", "", "")));
        }

        [Theory]
        [InlineData("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08", "")]
        [InlineData("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08", null)]
        [InlineData("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08", "123")]
        public async Task EditImage_ShouldSuccess(Guid bookId, string imageBase64)
        {
            _repository.Setup(s => s.GetAsync(bookId)).ReturnsAsync(DataSeed.Books.FirstOrDefault(b => b.Id == bookId));
            Book book = await _bookService.EditImage(bookId, imageBase64);

            Assert.Equal(bookId, book.Id);
            Assert.Equal(imageBase64, book.Image);
        }

        [Theory]
        [InlineData(null, "123")]
        [InlineData("58acac61-f9fa-46d3-a08f-08d8a5dad49a", "123")]
        public async Task EditImage_ShouldThrow_BookNotFoundException(Guid bookId, string imageBoom)
        {
            await Assert.ThrowsAsync<BookNotFoundException>(() => _bookService.EditImage(bookId, imageBoom));
        }

        [Theory]
        [InlineData("4f3a9241-4147-4adc-85be-08d8a7273f08", "")]
        [InlineData("0badcd1b-1dfe-4c5d-85ba-08d8a7273f08", null)]
        [InlineData("4f3a9241-4147-4adc-85be-08d8a7273f08", "123")]
        public async Task EditRemark_ShouldSuccess(Guid bookId, string remark)
        {
            _repository.Setup(s => s.GetAsync(bookId)).ReturnsAsync(DataSeed.Books.FirstOrDefault(b => b.Id == bookId));
            Book book = await _bookService.EditRemark(bookId, remark);

            Assert.Equal(bookId, book.Id);
            Assert.Equal(remark, book.Remark);
        }

        [Theory]
        [InlineData(null, "123")]
        [InlineData("58acac61-f9fa-46d3-a08f-08d8a5dad49a", "123")]
        public async Task EditRemark_ShouldThrow_BookNotFoundException(Guid bookId, string remark)
        {
            await Assert.ThrowsAsync<BookNotFoundException>(() => _bookService.EditRemark(bookId, remark));
        }
    }
}
