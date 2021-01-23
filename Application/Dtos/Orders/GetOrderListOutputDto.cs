using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Orders
{
    public class GetOrderListOutputDto : GetListOutputDto
    {
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public bool NeedInvoice { get; set; }
        public bool IsDeliver { get; set; }
        public bool IsClose { get; set; }
        public bool IsDone { get; set; }
    }
}
