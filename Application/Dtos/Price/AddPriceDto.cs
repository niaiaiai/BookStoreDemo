using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Price
{
    public class AddPriceDto
    {
        public string BookISBN { get; set; }
        public decimal Price { get; set; }
        public string Remark { get; set; }
    }
}
