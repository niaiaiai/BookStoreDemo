using Application.Dtos.Orders;
using AutoMapper;
using Domain.Entities;
using MyRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapperProfiles
{
    public class OrderAutoMapper : Profile
    {
        public OrderAutoMapper()
        {
            CreateMap<Order, GetOrderListOutputDto>();
        }
    }
}
