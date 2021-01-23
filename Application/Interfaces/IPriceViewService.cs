using Application.Dtos.Books;
using Application.Dtos.Price;
using Domain.Entities;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPriceViewService
    {
        public Task<IPageResult<GetPriceOutputDto>> GetPriceList(GetBookInputDto bookDto);

        //public Task EditBookPrice(UpdatePriceDto dto);

        public Task<BookPrice> AddBookPrice(AddPriceDto dto);
    }
}
