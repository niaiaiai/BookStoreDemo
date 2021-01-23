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

namespace DomainTest.Price
{
    public class GetBooksPrice : UnitTestBase<BookPrice, Guid>
    {
        private readonly PriceService _priceService;

        public GetBooksPrice() : base()
        {
            _priceService = new PriceService(_serviceProvider.Object);
        }

        [Theory]
        [MemberData(nameof(GetBookPrice))]
        public async Task GetBooksPrice_Should_GetList(PriceSpecification specification, int index, int pageSize, int count)
        {
            _repository.Setup(s => s.GetListAsync(It.IsAny<Expression<Func<BookPrice, bool>>>(), It.IsAny<Func<IQueryable<BookPrice>, IOrderedQueryable<BookPrice>>>(), It.IsAny<Expression<Func<BookPrice, object>>>()))
                .ReturnsAsync(DataSeed.BookPrices.AsQueryable().Where(specification));
            IPageResult<BookPrice> price = await _priceService.GetPriceList(specification, index, pageSize);
            Assert.NotNull(price);
            Assert.Equal(count, price.Total);
            int pageCount = count - pageSize * index;
            Assert.Equal(pageCount >= pageSize ? pageSize : (pageCount > 0 ? count : 0), price.Data.Count());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(0, -1)]
        public async Task GetBooksPrice_Should_ThrowException(int index, int pageSize)
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _priceService.GetPriceList(new PriceSpecification(), index, pageSize));
        }

        public static IEnumerable<object[]> GetBookPrice
            => new List<object[]> {
                new object[] { new PriceSpecification(new[] { Guid.Parse("e3fac700-ed2a-4258-85bd-08d8a7273f08") }), 0, 10, 2 }
            };
    }
}
