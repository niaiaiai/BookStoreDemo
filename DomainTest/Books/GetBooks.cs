using Domain.Entities;
using Domain.Services;
using Domain.Specifications;
using Infrastructure;
using Moq;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace DomainTest.Books
{
    public class GetBooks : UnitTestBase<Book, Guid>
    {
        private BookService _bookService;

        public GetBooks() : base()
        {
            _bookService = new BookService(_serviceProvider.Object);
        }

        [Theory]
        [MemberData(nameof(GetBookSpecification))]
        [MemberData(nameof(GetBookAuthorSpecification))]
        [MemberData(nameof(GetBookTitleSpecification))]
        [MemberData(nameof(GetBookPublisherSpecification))]
        [MemberData(nameof(GetBookTypeSpecification))]
        public async Task GetBooks_ShouldGet_CorrectBookList(BookSpecification specification, int index, int pageSize, int count)
        {
            IQueryable<Book> bookQueryable = DataSeed.Books.AsQueryable().Where(specification);
            _repository.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(), It.IsAny<Expression<Func<Book, object>>>()))
                .ReturnsAsync(bookQueryable);
            IPageResult<Book> books = await _bookService.GetBooks(specification, index, pageSize);

            Assert.NotNull(books);
            Assert.Equal(count, books.Total);
            int pageCount = count - pageSize * index;
            Assert.Equal(pageCount >= pageSize ? pageSize : (pageCount > 0 ? count : 0), books.Data.Count());
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(0, -1)]
        public async Task GetBooks_ShouldGet_BookListThrowException(int index, int pageSize)
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _bookService.GetBooks(new BookSpecification(), index, pageSize));
        }

        public static IEnumerable<object[]> GetBookSpecification
            => new List<object[]> {
                new object[] { new BookSpecification("", "垣野内成美", null, "", -1), 0, 1, 2 }
            };
        public static IEnumerable<object[]> GetBookAuthorSpecification
            => new List<object[]> {
                new object[] { new BookSpecification(null, "垣野内成美", "", null, 2), 0, 10, 2 }
            };
        public static IEnumerable<object[]> GetBookTitleSpecification
            => new List<object[]> {
                new object[] { new BookSpecification("c", "", "", null, 1), 0, 10, 2 }
            };
        public static IEnumerable<object[]> GetBookPublisherSpecification
            => new List<object[]> {
                new object[] { new BookSpecification(" ", "", "出版社", null, 1), 0, 2, 3 }
            };
        public static IEnumerable<object[]> GetBookTypeSpecification
            => new List<object[]> {
                new object[] { new BookSpecification(" ", "", "", null, 1), 2, 1, 3 }
            };
    }
}
