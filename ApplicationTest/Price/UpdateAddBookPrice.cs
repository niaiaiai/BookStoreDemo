using Application.Dtos.Price;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure;
using Moq;
using MyRepositories.UnitOfWork;
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
    public class UpdateAddBookPrice : UnitTestBase<Book, Guid>
    {
        private readonly Mock<IPriceService> _priceService;
        private readonly PriceViewService _priceViewService;
        public UpdateAddBookPrice() : base()
        {
            _priceService = new();
            _serviceProvider.Setup(s => s.GetService(typeof(IPriceService))).Returns(_priceService.Object);
            _priceViewService = new(_serviceProvider.Object);
        }

        [Theory]
        [MemberData(nameof(AddBookPrice1))]
        [MemberData(nameof(AddBookPrice2))]
        public async Task AddBookPrice_Should_Success(AddPriceDto dto)
        {
            _repository.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<Expression<Func<Book, object>>>()))
                .ReturnsAsync(DataSeed.Books.FirstOrDefault(b => b.ISBN == dto.BookISBN));
            _priceService.Setup(s => s.AddBookPrice(Guid.NewGuid(), 0m, "")).ReturnsAsync(new BookPrice(Guid.NewGuid(), 0m, ""));
            await _priceViewService.AddBookPrice(dto);
        }


        [Theory]
        [InlineData("aaaaaaaaaaaaa")]
        public async Task AddBookPrice_Should_ThrowBookNotFoundException(string isbn)
        {
            _repository.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<Expression<Func<Book, object>>>()))
                .ReturnsAsync(DataSeed.Books.FirstOrDefault(b => b.ISBN == isbn));
            await Assert.ThrowsAsync<BookNotFoundException>(() => _priceViewService.AddBookPrice(Mock.Of<AddPriceDto>()));
        }

        //[Theory]
        //[MemberData(nameof(UpdateBookPrice))]
        //public async Task EditBookPrice_Should_Success(UpdatePriceDto dto)
        //{
        //    _priceService.Setup(s => s.EditBookPrice(Guid.NewGuid(), 0m)).ReturnsAsync(new BookPrice(Guid.NewGuid(), 0m, ""));
        //    await _priceViewService.EditBookPrice(dto);
        //}

        public static IEnumerable<object[]> AddBookPrice1
            => new List<object[]> {
                    new object[] { new AddPriceDto() { BookISBN = "1234",  Price = 3, Remark = "添加测试" } }
            };
        public static IEnumerable<object[]> AddBookPrice2
            => new List<object[]> {
                    new object[] { new AddPriceDto() { BookISBN = "9784253177719",  Price = 10.05m, Remark = null } }
            };
        //public static IEnumerable<object[]> UpdateBookPrice
        //    => new List<object[]> {
        //            new object[] { new UpdatePriceDto() { Id = Guid.Parse("d83babcc-47cb-43c1-9755-294e3fb9d6ce"),  Price = 10.05m } }
        //    };
    }
}
