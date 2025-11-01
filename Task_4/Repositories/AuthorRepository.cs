using Microsoft.EntityFrameworkCore;
using Task_4.Data;
using Task_4.Models;

namespace Task_4.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {

        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context) 
        {
            _context = context;
        }

        public IEnumerable<Author> GetAll()
        {

            return _context.Authors.ToList();
        }

        public Author GetById(int id)
        {

            return _context.Authors.Find(id);
        }

        public void Create(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void Update(Author author)
        {
            _context.Authors.Update(author);  
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = GetById(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }

        public IEnumerable<object> GetAllAuthorsWithBookCount()
        {
            return _context.Authors
                           .Include(a => a.Books) 
                           .Select(a => new
                           {
                               AuthorName = a.Name,
                               BookCount = a.Books.Count()
                           })
                           .ToList();
        }

        public IEnumerable<Author> FindAuthorsByName(string namePart)
        {
            return _context.Authors
                           .Where(a => a.Name.Contains(namePart))
                           .ToList();
        }
    }
}

