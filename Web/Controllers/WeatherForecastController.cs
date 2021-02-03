using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public WeatherForecastController(IServiceProvider serviceProvider, BookStoreContext context)
        {
            var _mapper = serviceProvider.GetService<IInvoiceService>();
        }

        [HttpGet]
        public async Task Get()
        {
            
            
        }
    }
}
