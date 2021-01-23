using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Invoices
{
    public class GetInvoiceListOutputDto : GetListOutputDto
    {
        public DateTime InvoiceDate { get; set; }
        public string InvoiceCode { get; set; }
        public string InvoiceNo { get; set; }
        public string OrderNo { get; set; }
        public string Drawer { get; set; }
        public bool IsRed { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
    }
}
