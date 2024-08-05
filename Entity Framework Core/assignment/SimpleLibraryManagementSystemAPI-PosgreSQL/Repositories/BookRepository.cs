using Microsoft.EntityFrameworkCore;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Data;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Interfaces;
using SimpleLibraryManagementSystemAPI_PosgreSQL.Models;

namespace SimpleLibraryManagementSystemAPI_PosgreSQL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book? GetBookbyId(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            return book;
        }

        public void CreateBook(Book book)
        {
            // var newBook = new Book()
            // {
            //     Title = book.Title,
            //     Author = book.Author,
            //     PublicationYear = book.PublicationYear,
            //     ISBN = book.ISBN
            // };

            _context.Books.Add(book);
            // return book;
        }

        public void UpdateBook(Book inputBook)
        {
            // var bookToBeUpdated = GetBookbyId(id);

            // if (bookToBeUpdated == null)
            // {
            //     return null;
            // }

            // bookToBeUpdated.
            _context.Entry(inputBook).State = EntityState.Modified;
            
        }

        public void DeleteBook(int id)
        {
            var bookToBeDeleted = GetBookbyId(id);

            if (bookToBeDeleted != null)
            {
                _context.Books.Remove(bookToBeDeleted);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}