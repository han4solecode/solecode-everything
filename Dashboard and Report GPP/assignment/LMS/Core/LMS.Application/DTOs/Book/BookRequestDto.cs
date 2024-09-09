namespace LMS.Application.DTOs.Book
{
    public class BookRequestDto
    {
        public string? Title { get; set; }

        public string? ISBN { get; set; }

        public string? Author { get; set; }

        public string? Publisher { get; set; }
    }
}