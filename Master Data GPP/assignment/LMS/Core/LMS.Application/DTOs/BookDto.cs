namespace LMS.Application.DTOs
{
    public class BookDto
    {
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string ISBN { get; set;} = null!;

        public string Publisher { get; set; } = null!;

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? Language { get; set; }

        public string Location { get; set; } = null!;

        public DateOnly PurchaseDate { get; set; }

        public decimal Price { get; set; }
    }
}