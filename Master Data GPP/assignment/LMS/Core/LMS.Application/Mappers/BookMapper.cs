using LMS.Application.DTOs;
using LMS.Domain.Entities;

namespace LMS.Application.Mappers
{
    public static class BookMapper
    {
        public static BookDto GetBookResponse(this Book book)
        {
            return new BookDto
            {
                Category = book.Category,
                Title = book.Title,
                ISBN = book.ISBN,
                Publisher = book.Publisher,
                Description = book.Description,
                Location = book.Location,
            };
        }
    }
}