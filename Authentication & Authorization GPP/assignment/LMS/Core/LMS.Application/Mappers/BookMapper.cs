using LMS.Application.DTOs;
using LMS.Domain.Entities;

namespace LMS.Application.Mappers
{
    public static class BookMapper
    {
        public static BookDto AsBookDto(this Book book)
        {
            return new BookDto
            {
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                ISBN = book.ISBN,
                Publisher = book.Publisher,
                Location = book.Location,
            };
        }
    }
}