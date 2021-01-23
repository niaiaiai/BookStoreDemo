using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyRepositories.Repositories;
using MyRepositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //return await _bookService.GetBooks(new BookSpecification(), 0, -10);
        }
    }
}
