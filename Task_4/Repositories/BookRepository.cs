using Microsoft.EntityFrameworkCore;
using Task_4.Data;
using Task_4.Models;

namespace Task_4.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {

            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {

            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public void Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Book> GetBooksByAuthorId(int authorId)
        {
            return _context.Books.Where(b => b.AuthorId == authorId).ToList();
        }

        public IEnumerable<Book> GetBooksPublishedAfterYear()
        {
            return _context.Books
                           .Where(b => b.PublishedYear > 2015)
                           .ToList();
        }
    }
}