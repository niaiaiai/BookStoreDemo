using Domain.Entities;
using Domain.Specifications;
using MyServices.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPriceService
    {
        public Task<IPageResult<BookPrice>> GetPriceList(PriceSpecification priceSpecification, int index, int pageSize);

        public Task<BookPrice> EditBookPrice(Guid bookPriceId, decimal price);

        public Task<BookPrice> AddBookPrice(Guid bookId, decimal price, string remark);
    }
}
