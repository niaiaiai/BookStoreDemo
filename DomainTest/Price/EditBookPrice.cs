using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Infrastructure;
using InfrastructureTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using MyRepositories.Repositories;
using MyRepositories.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestBase;
using Xunit;

namespace DomainTest.Price
{
    public class EditBookPrice : UnitTestBase<BookPrice, Guid>
    {
        private readonly Mock<IRepository<Book, Guid>> _bookRepository;
        private readonly PriceService _priceService;

        public EditBookPrice() : base()
        {
            _bookRepository = new();
            _serviceProvider.Setup(s => s.GetService(typeof(IRepository<Book, Guid>))).Returns(_bookRepository.Object);
            _priceService = new PriceService(_serviceProvider.Object);
        }

        [Theory]
        [InlineData("d83babcc-47cb-43c1-9755-294e3fb9d6ce", 3)]
        [InlineData("d83babcc-47cb-43c1-9755-294e3fb9d6ce", 10)]
        public async Task EditBookPrice_ShouldSuccess(Guid bookPriceId, decimal price)
        {
            _repository.Setup(s => s.GetAsync(bookPriceId)).ReturnsAsync(DataSeed.BookPrices.FirstOrDefault(bp => bp.Id == bookPriceId));
            _repository.Setup(s => s.UpdateAsync(new BookPrice(Guid.NewGuid(), 0m, "")));
            BookPrice bookPrice = await _priceService.EditBookPrice(bookPriceId, price);

            Assert.Equal(bookPriceId, bookPrice.Id);
            Assert.Equal(price, bookPrice.Price);
        }

        [Theory]
        [InlineData("d83babcc-47cb-43c1-9755-294e3fb9daaa", 3)]
        public async Task EditBookPrice_Should_ThrowBookPriceNotFoundException(Guid bookPriceId, decimal price)
        {
            _repository.Setup(s => s.GetAsync(bookPriceId)).ReturnsAsync(DataSeed.BookPrices.FirstOrDefault(bp => bp.Id == bookPriceId));
            await Assert.ThrowsAsync<BookPriceNotFoundException>(() => _priceService.EditBookPrice(bookPriceId, price));
        }

        [Theory]
        [InlineData("d83babcc-47cb-43c1-9755-294e3fb9d6ce", -0.00001)]
        public async Task EditBookPrice_Should_ThrowBookPriceOutOfRangeException(Guid bookPriceId, decimal price)
        {
            await Assert.ThrowsAsync<BookPriceOutOfRangeException>(() => _priceService.EditBookPrice(bookPriceId, price));
        }

        [Theory]
        [InlineData("68fb9bf4-58b6-49b6-85bb-08d8a7273f08", 3, "添加测试")]
        [InlineData("4f3a9241-4147-4adc-85be-08d8a7273f08", 10.05, null)]
        public async Task AddBookPrice_ShouldSuccess(Guid bookId, decimal price, string remark)
        {
            _bookRepository.Setup(s => s.GetAsync(bookId)).ReturnsAsync(DataSeed.Books.FirstOrDefault(b => b.Id == bookId));
            _repository.Setup(s => s.InsertAsync(new BookPrice(Guid.NewGuid(), 0m, "")));
            BookPrice bookPrice = await _priceService.AddBookPrice(bookId, price, remark);

            Assert.Equal(bookId, bookPrice.BookId);
            Assert.Equal(price, bookPrice.Price);
            Assert.Equal(remark, bookPrice.Remark);
        }

        [Theory]
        [InlineData("4f3a9241-4147-4adc-85be-08d8a7273f08", -0.00001, null)]
        public async Task AddBookPrice_Should_ThrowBookPriceOutOfRangeException(Guid bookId, decimal price, string remark)
        {
            await Assert.ThrowsAsync<BookPriceOutOfRangeException>(() => _priceService.AddBookPrice(bookId, price, remark));
        }
    }
}
