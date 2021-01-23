using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Books
{
    public class UpdateBookDto
    {
        public Guid Id { get; set; }

        public string ImageBase64 { get; set; }

        public string Remark { get; set; }
    }
}
