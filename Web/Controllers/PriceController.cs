using Application.Dtos.Books;
using Application.Dtos.Price;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyServices.Dtos;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IPriceViewService _priceViewService;
        private readonly IPriceService _priceService;
        public PriceController(IPriceViewService priceViewService, IPriceService priceService)
        {
            _priceViewService = priceViewService;
            _priceService = priceService;
        }

        [HttpGet]
        public async Task<ActionResult<IPageResult<GetPriceOutputDto>>> GetPriceList([FromQuery]GetBookInputDto dto)
        {
            var result = await _priceViewService.GetPriceList(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookPrice>> AddBookPrice(AddPriceDto dto)
        {
            BookPrice bookPrice = await _priceViewService.AddBookPrice(dto);
            return CreatedAtAction(nameof(GetPriceList), new { });
        }

        [HttpPut]
        public async Task<ActionResult> EditBookPrice(UpdatePriceDto dto)
        {
            await _priceService.EditBookPrice(dto.Id, dto.Price);
            return Ok();
        }
    }
}
