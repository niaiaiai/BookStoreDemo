using Application.Dtos.Books;
using Application.Dtos.Price;
using Application.Interfaces;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using MyRepositories.Repositories;
using MyRepositories.UnitOfWork;
using MyServices.Dtos;
using MyServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PriceViewService : ApplicationService, IPriceViewService
    {
        private readonly IPriceService _priceService;
        private readonly IRepository<Book, Guid> _bookRepository;

        public PriceViewService(IServiceProvider provider) : base(provider)
        {
            _priceService = provider.GetService<IPriceService>();
            _bookRepository = provider.GetService<IRepository<Book, Guid>>();
        }

        public async Task<BookPrice> AddBookPrice(AddPriceDto dto)
        {
            Book book = await _bookRepository.GetAsync(b => b.ISBN == dto.BookISBN.Trim());
            Guard.Against.BookNotFound(book, dto.BookISBN);
            return await _priceService.AddBookPrice(book.Id, dto.Price, dto.Remark);
        }

        //public async Task EditBookPrice(UpdatePriceDto dto)
        //{
        //    await _priceService.EditBookPrice(dto.Id, dto.Price);
        //}

        public async Task<IPageResult<GetPriceOutputDto>> GetPriceList(GetBookInputDto dto)
        {
            Guard.Against.NegativeIndexPage(dto.PageSize, dto.Index);
            BookSpecification bookSpecification = new(dto.Title, dto.Author, dto.Publisher, dto.ISBN, dto.BookTypeId);
            var books = (await _bookRepository.GetListAsync(bookSpecification)).ToList();
            IPageResult<BookPrice> price = await _priceService.GetPriceList(new PriceSpecification(books.Select(s => s.Id)), dto.Index, dto.PageSize);

            IEnumerable<GetPriceOutputDto> outputDto = price.Data.Join(books, bp => bp.BookId, b => b.Id, (bp, b)
                => new GetPriceOutputDto { BookTitle = b.Title, ISBN = b.ISBN, Price = bp.Price, Remark = bp.Remark, Id = bp.Id });

            return new PageResult<GetPriceOutputDto> { Total = price.Total, Data = outputDto };
        }
    }
}
