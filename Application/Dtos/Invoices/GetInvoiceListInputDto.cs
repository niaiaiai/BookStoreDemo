using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Invoices
{
    public class GetInvoiceListInputDto : GetListInputDto
    {
        public string InvoiceCodeNo { get; set; }
        public string Drawer { get; set; }
        public string OrderNo { get; set; }
        public bool? IsRed { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now.AddMonths(-3);
        public DateTime EndTime { get; set; } = DateTime.Now;
    }
}
