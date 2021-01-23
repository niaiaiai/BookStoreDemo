using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Orders
{
    public class GetOrderListInputDto : GetListInputDto
    {
        public string OrderNo { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now.AddMonths(-3);
        public DateTime EndTime { get; set; } = DateTime.Now;
        public bool? IsClose { get; set; }
        public bool? IsDone { get; set; }
        public bool? NeedInvoice { get; set; }
        public bool? IsDeliver { get; set; }
    }
}
