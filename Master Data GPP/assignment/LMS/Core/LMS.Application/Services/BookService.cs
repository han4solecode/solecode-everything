using LMS.Application.Contracts;
using LMS.Application.Persistance;
using LMS.Application.Persistance.Helper;
using LMS.Domain.Entities;

namespace LMS.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepostory _bookRepostory;

        public BookService(IBookRepostory bookRepostory)
        {
            _bookRepostory = bookRepostory;
        }

        public async Task<bool> AddNewBook(Book book)
        {
            try
            {
                await _bookRepostory.Create(book);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public async Task<bool> DeleteExistingBook(int id)
        {
            var book = await _bookRepostory.GetById(id);

            if (book == null)
            {
                return false;
            }

            try
            {
                await _bookRepostory.Delete(book);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooks(int a, int b)
        {
            var books = await _bookRepostory.GetAll(a, b);

            return books;
        }

        public async Task<Book?> GetBookById(int id)
        {
            var book = await _bookRepostory.GetById(id);

            return book;
        }

        public async Task<bool> UpdateExistingBook(int id, Book inputBook)
        {
            var book = await _bookRepostory.GetById(id);

            if (book == null)
            {
                return false;
            }

            book.Author = inputBook.Author;
            book.Category = inputBook.Category;
            book.Description = inputBook.Description;
            book.ISBN = inputBook.ISBN;
            book.Location = inputBook.Location;
            book.Price = inputBook.Price;
            book.Publisher = inputBook.Publisher;
            book.PurchaseDate = inputBook.PurchaseDate;
            book.Title = inputBook.Title;

            await _bookRepostory.Update(book);

            return true;
        }

        public async Task<IEnumerable<Book>> SearchBookByQuery(QueryObject query, int a, int b)
        {
            var books = await _bookRepostory.SearchBook(query, a, b);

            return books;
        }

        public async Task<bool> BookSoftDelete(int id, string? reason)
        {
            var book = await _bookRepostory.GetById(id);

            if (book == null)
            {
                return false;
            }

            try
            {
                await _bookRepostory.BookSoftDelete(book, reason);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}