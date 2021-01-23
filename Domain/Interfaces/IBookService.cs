using Domain.Entities;
using Domain.Specifications;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookService
    {
        public Task<IPageResult<Book>> GetBooks(BookSpecification specification, int index, int pageSize);

        public Task<Book> EditImage(Guid bookId, string imageBase64);

        public Task<Book> EditRemark(Guid bookId, string remark);
    }
}
