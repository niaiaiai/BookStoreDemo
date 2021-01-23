using System;

namespace Application.Dtos.Books
{
    public class GetBookOutputDto : GetListOutputDto
    {

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string Image { get; set; }

        public string Publisher { get; set; }

        public string Author { get; set; }

        public int BookTypeId { get; set; }

        public string Remark { get; set; }
    }
}
