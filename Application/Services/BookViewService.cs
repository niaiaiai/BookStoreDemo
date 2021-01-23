using Application.AutoMapperProfiles;
using Application.Dtos;
using Application.Dtos.Books;
using Application.Interfaces;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using MyServices.Dtos;
using MyServices.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookViewService : ApplicationService, IBookViewService
    {
        private readonly IBookService _bookService;

        public BookViewService(IServiceProvider provider) : base(provider)
        {
            _bookService = provider.GetService<IBookService>();
        }

        //public Task EditImage(UpdateBookDto dto)
        //{
        //    return _bookService.EditImage(dto.Id, dto.ImageBase64);
        //}

        //public Task EditRemark(UpdateBookDto dto)
        //{
        //    return _bookService.EditRemark(dto.Id, dto.Remark);
        //}

        public async Task<IPageResult<GetBookOutputDto>> GetBookList(GetBookInputDto dto)
        {
            Guard.Against.NegativeIndexPage(dto.PageSize, dto.Index);
            BookSpecification bookSpecification = new(dto.Title, dto.Author, dto.Publisher, dto.ISBN, dto.BookTypeId);
            IPageResult<Book> books = await _bookService.GetBooks(bookSpecification, dto.Index, dto.PageSize);

            return new PageResult<GetBookOutputDto> { Total = books.Total, Data = _mapper.Map<List<GetBookOutputDto>>(books.Data) };
        }
    }
}
