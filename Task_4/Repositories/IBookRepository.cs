using Task_4.Models;

namespace Task_4.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Create(Book book);
        void Update(Book book);
        void Delete(int id);
        IEnumerable<Book> GetBooksByAuthorId(int authorId);
        IEnumerable<Book> GetBooksPublishedAfterYear();
    }
}
