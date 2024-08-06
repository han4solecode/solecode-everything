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

        public Book CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book? UpdateBook(int id, Book inputBook)
        {
            var bookToBeUpdated = GetBookbyId(id);

            if (bookToBeUpdated == null)
            {
                return null;
            }

            // _context.Entry(inputBook).State = EntityState.Modified;
            bookToBeUpdated.Title = inputBook.Title;
            bookToBeUpdated.Author = inputBook.Author;
            bookToBeUpdated.PublicationYear = inputBook.PublicationYear;
            bookToBeUpdated.ISBN = inputBook.ISBN;

            _context.Books.Update(bookToBeUpdated);
            _context.SaveChanges();
            return bookToBeUpdated;
        }

        public bool DeleteBook(int id)
        {
            var bookToBeDeleted = GetBookbyId(id);

            if (bookToBeDeleted == null)
            {
                return false;
            }

            _context.Books.Remove(bookToBeDeleted);
            _context.SaveChanges();
            return true;
        }

        // public void Save()
        // {
        //     _context.SaveChanges();
        // }
    }
}