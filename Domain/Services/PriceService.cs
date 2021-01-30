using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using MyRepositories.Repositories;
using MyServices.Dtos;
using MyServices.Services;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PriceService : DomainService, IPriceService
    {
        private readonly IRepository<BookPrice, Guid> _priceRepository;
        public PriceService(IServiceProvider provider) : base(provider)
        {
            _priceRepository = provider.GetService<IRepository<BookPrice, Guid>>();
        }

        public async Task<BookPrice> AddBookPrice(Guid bookId, decimal price, string remark)
        {
            Guard.Against.NegativePrice(price);

            BookPrice bookPrice = new BookPrice(bookId, price, remark);
            await _priceRepository.InsertAsync(bookPrice);
            return bookPrice;
        }

        public async Task<BookPrice> EditBookPrice(Guid bookPriceId, decimal price)
        {
            Guard.Against.NegativePrice(price);
            BookPrice bookPrice = await _priceRepository.GetAsync(bookPriceId);
            Guard.Against.BookPriceNotFound(bookPrice, bookPriceId);

            bookPrice.Price = price;
            await _priceRepository.UpdateAsync(bookPrice);
            return bookPrice;
        }

        public async Task<IPageResult<BookPrice>> GetPriceList(PriceSpecification priceSpecification, int index, int pageSize)
        {
            Guard.Against.NegativeIndexPage(pageSize, index);

            var bookPrice = await _priceRepository.GetListAsync(priceSpecification);
            return new PageResult<BookPrice>(bookPrice, index, pageSize);
        }
    }
}
