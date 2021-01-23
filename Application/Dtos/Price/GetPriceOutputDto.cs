using System;

namespace Application.Dtos.Price
{
    public class GetPriceOutputDto : GetListOutputDto
    {
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public string Remark { get; set; }
    }
}
