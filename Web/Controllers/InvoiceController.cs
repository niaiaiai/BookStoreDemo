using Application.Dtos.Invoices;
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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IInvoiceViewService _invoiceViewService;

        public InvoiceController(IInvoiceService invoiceService, IInvoiceViewService invoiceViewService)
        {
            _invoiceService = invoiceService;
            _invoiceViewService = invoiceViewService;
        }

        [HttpGet]
        public async Task<ActionResult<IPageResult<GetInvoiceListOutputDto>>> GetInvoices([FromQuery]GetInvoiceListInputDto dto)
        {
            var result = await _invoiceViewService.GetInvoiceList(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice(AddInvoiceDto dto)
        {
            Invoice invoice = await _invoiceViewService.AddInvoice(dto);
            return CreatedAtAction(nameof(AddInvoice), new { id = invoice.Id }, invoice);
        }
    }
}
