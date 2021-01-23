using Application.Dtos.Books;
using AutoMapper;
using Domain.Entities;
using MyRepositories;

namespace Application.AutoMapperProfiles
{
    public class BookAutoMapper : Profile
    {
        public BookAutoMapper()
        {
            CreateMap<Book, GetBookOutputDto>();
        }
    }
}
