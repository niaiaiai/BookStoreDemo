using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyRepositories.Repositories;
using MyRepositories.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController(IServiceProvider serviceProvider)
        {
            var _mapper = serviceProvider.GetService<BookStoreContext>();
            var aa = serviceProvider.GetRequiredService<IDataSeed>();
        }

        [HttpGet]
        public async Task Get()
        {
            
            
        }
    }
}
