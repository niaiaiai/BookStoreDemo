namespace Application.Dtos.Books
{
    public class GetBookInputDto : GetListInputDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Publisher { get; set; }

        public int BookTypeId { get; set; } = -1;
    }
}
