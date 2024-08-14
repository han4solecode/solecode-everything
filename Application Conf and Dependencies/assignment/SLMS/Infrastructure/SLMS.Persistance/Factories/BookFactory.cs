using SLMS.Domain.Entities;

namespace SLMS.Persistance.Factories
{
    public class BookFactory
    {
        public Book CreateItem(Book bookData)
        {
            return new Book
            {
                Title = bookData.Title,
                Author = bookData.Author,
                Publicationyear = bookData.Publicationyear,
                Isbn = bookData.Isbn
            };
        }
    }
}