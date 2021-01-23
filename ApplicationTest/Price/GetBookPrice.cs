using Application.Dtos.Books;
using Application.Dtos.Price;
using Application.Services;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace ApplicationTest.Price
{
    public class GetBookPrice : UnitTestBase<Book, Guid>
    {
        private Mock<IPriceService> _priceService;
        private PriceViewService _priceViewService;

        public GetBookPrice() : base()
        {
            _priceService = new();
            _serviceProvider.Setup(s => s.GetService(typeof(IPriceService))).Returns(_priceService.Object);
            _priceViewService = new(_serviceProvider.Object);
        }

        [Theory]
        [MemberData(nameof(GetBookPriceByTitle))]
        [MemberData(nameof(GetBookPriceByNoResult))]
        [MemberData(nameof(GetBookPriceByISBN))]
        [MemberData(nameof(GetBookPriceByAll))]
        [MemberData(nameof(GetBookPriceByPagination))]
        public async Task GetPriceList_Should_GetListDto(GetBookInputDto dto, int count)
        {
            BookSpecification bookSpecification = new(dto.Title, dto.Author, dto.Publisher, dto.ISBN, dto.BookTypeId);
            var books = DataSeed.Books.AsQueryable().Where(bookSpecification);
            PriceSpecification priceSpecification = new(books.Select(s => s.Id));
            var prices = DataSeed.BookPrices.AsQueryable().Where(priceSpecification);

            _repository.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(), It.IsAny<Expression<Func<Book, object>>>()))
                .ReturnsAsync(books);
            _priceService.Setup(s => s.GetPriceList(It.IsAny<PriceSpecification>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PageResult<BookPrice>(prices, dto.Index, dto.PageSize));

            IPageResult<GetPriceOutputDto> result = await _priceViewService.GetPriceList(dto);

            Assert.NotNull(result);
            Assert.Equal(count, result.Total);
            Assert.NotNull(result.Data);
            int pageCount = count - dto.PageSize * dto.Index;
            Assert.Equal(pageCount >= dto.PageSize ? dto.PageSize : (pageCount > 0 ? count : 0), result.Data.Count());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(0, -1)]
        public async Task GetPriceList_Should_ThrowException(int index, int pageSize)
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _priceViewService.GetPriceList(new GetBookInputDto() { Index = index, PageSize = pageSize }));
        }

        public static IEnumerable<object[]> GetBookPriceByTitle
            => new List<object[]> {
                new object[] { new GetBookInputDto { Title = "海贼王", Index = 0, PageSize = 10 }, 2 }
            };
        public static IEnumerable<object[]> GetBookPriceByNoResult
            => new List<object[]> {
                new object[] { new GetBookInputDto { Title = "海贼王", ISBN = "9787111618331", Index = 0, PageSize = 10 }, 0 }
            };
        public static IEnumerable<object[]> GetBookPriceByISBN
            => new List<object[]> {
                new object[] { new GetBookInputDto { Title = "设计模式", ISBN = "9787111618331", Index = 0, PageSize = 10 }, 2 }
            };
        public static IEnumerable<object[]> GetBookPriceByAll
            => new List<object[]> {
                new object[] { new GetBookInputDto { Index = 0, PageSize = 10 }, DataSeed.BookPrices.Count }
            };
        public static IEnumerable<object[]> GetBookPriceByPagination
            => new List<object[]> {
                new object[] { new GetBookInputDto { Index = 5, PageSize = 5 }, DataSeed.BookPrices.Count }
            };
    }
}
