using Application.Dtos;
using Application.Dtos.Books;
using MyServices.Dtos;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookViewService
    {
        public Task<IPageResult<GetBookOutputDto>> GetBookList(GetBookInputDto bookDto);

        //public Task EditImage(UpdateBookDto dto);

        //public Task EditRemark(UpdateBookDto dto);
    }
}
