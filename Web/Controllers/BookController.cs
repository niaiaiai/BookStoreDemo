using Application.Dtos.Books;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyServices.Dtos;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class BookController : BaseController
    {
        private readonly IBookViewService _bookViewService;
        private readonly IBookService _bookService;
        public BookController(IBookViewService bookViewService, IBookService bookService)
        {
            _bookViewService = bookViewService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IPageResult<GetBookOutputDto>>> GetBooks([FromQuery]GetBookInputDto dto)
        {
            var result = await _bookViewService.GetBookList(dto);
            return Ok(result);
        }

        [HttpPut("image")]
        public async Task<ActionResult> EditImage(UpdateBookDto dto)
        {
            await _bookService.EditImage(dto.Id, dto.ImageBase64);
            return Ok();
        }

        [HttpPut("remark")]
        public async Task<ActionResult> EditRemark(UpdateBookDto dto)
        {
            await _bookService.EditRemark(dto.Id, dto.Remark);
            return Ok();
        }
    }
}
