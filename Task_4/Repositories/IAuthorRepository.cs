using Task_4.Models;

namespace Task_4.Repositories
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author GetById(int id);
        void Create(Author author);
        void Update(Author author);
        void Delete(int id);
        IEnumerable<object> GetAllAuthorsWithBookCount();

        IEnumerable<Author> FindAuthorsByName(string namePart);
    }
}
