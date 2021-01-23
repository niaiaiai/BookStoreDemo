using Application.Dtos;
using Application.Dtos.Books;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Domain.Specifications;
using Infrastructure;
using InfrastructureTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using MyRepositories.UnitOfWork;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace ApplicationTest.Books
{
    public class GetBooks : UnitTestBase<Book, Guid>
    {
        private Mock<IBookService> _bookService;
        private BookViewService _bookViewService;

        public GetBooks() : base()
        {
            _bookService = new ();
            IMapper mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, GetBookOutputDto>();
            }).CreateMapper();
            _serviceProvider.Setup(s => s.GetService(typeof(IBookService))).Returns(_bookService.Object);
            _serviceProvider.Setup(s => s.GetService(typeof(IMapper))).Returns(mapper);

            _bookViewService = new (_serviceProvider.Object);

        }

        [Theory]
        [MemberData(nameof(GetBookSpecification))]
        [MemberData(nameof(GetBookAuthorSpecification))]
        [MemberData(nameof(GetBookTitleSpecification))]
        [MemberData(nameof(GetBookPublisherSpecification))]
        [MemberData(nameof(GetBookTypeSpecification))]
        public async Task GetBooks_ShouldGet_CorrectBookList(GetBookInputDto dto, int count)
        {
            BookSpecification bookSpecification = new(dto.Title, dto.Author, dto.Publisher, dto.ISBN, dto.BookTypeId);
            var bookList = DataSeed.Books.AsQueryable().Where(bookSpecification);
            
            _bookService.Setup(s => s.GetBooks(It.IsAny<BookSpecification>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PageResult<Book>(bookList, dto.Index, dto.PageSize));

            IPageResult<GetBookOutputDto> books = await _bookViewService.GetBookList(dto);

            Assert.NotNull(books);
            Assert.Equal(count, books.Total);
            Assert.NotNull(books.Data);
            int pageCount = count - dto.PageSize * dto.Index;
            Assert.Equal(pageCount >= dto.PageSize ? dto.PageSize : (pageCount > 0 ? count : 0), books.Data.Count());
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(0, -1)]
        public async Task GetBooks_ShouldGet_BookListThrowException(int index, int pageSize)
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _bookViewService.GetBookList(new GetBookInputDto() { Index = index, PageSize = pageSize }));
        }

        public static IEnumerable<object[]> GetBookSpecification
            => new List<object[]> {
                    new object[] { new GetBookInputDto() { Author = "垣野内成美", Index = 0, PageSize = 1 }, 2 }
            };
        public static IEnumerable<object[]> GetBookAuthorSpecification
            => new List<object[]> {
                    new object[] { new GetBookInputDto() { Author = "垣野内成美", BookTypeId = 2, Index = 0, PageSize = 10 }, 2 }
            };
        public static IEnumerable<object[]> GetBookTitleSpecification
            => new List<object[]> {
                    new object[] { new GetBookInputDto() { Title = "c", BookTypeId = 1, Index = 0, PageSize = 10 }, 2 }
            };
        public static IEnumerable<object[]> GetBookPublisherSpecification
            => new List<object[]> {
                    new object[] { new GetBookInputDto() { Publisher = "出版社", BookTypeId = 1, Index = 0, PageSize = 2 }, 3 }
            };
        public static IEnumerable<object[]> GetBookTypeSpecification
            => new List<object[]> {
                    new object[] { new GetBookInputDto() { BookTypeId = 1, Index = 2, PageSize = 1 }, 3 }
            };
    }
}
