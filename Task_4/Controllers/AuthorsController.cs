using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_4.Data;
using Task_4.Models;
using Task_4.DTOs.Author;  

namespace Task_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<AuthorDTO>> GetAllAuthorsWithBooks()
        {
            var authors = _context.Authors
                .Include(a => a.Books)
                .ToList()
                .Select(a => MapToDTO(a))
                .ToList();

            return Ok(authors);
        }


        [HttpGet("{id}")]
        public ActionResult<AuthorDTO> GetAuthorById(int id)
        {
            var author = _context.Authors
                 .Include(a => a.Books)
                 .FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound($"Don't exist author with id {id}");
            }

            return Ok(MapToDTO(author));

        }


        [HttpGet("authors-with-book-count")]
        public ActionResult<IEnumerable<AuthorWithBookCountDTO>> GetAuthorsWithCountOfBooks()
        {
            var authors = _context.Authors
                .Include(a => a.Books)
                .ToList()
                .Select(a => new AuthorWithBookCountDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    DateOfBirth = a.DateOfBirth,
                    BookCount = a.Books.Count
                })
                .ToList();

            if (!authors.Any())
            {
                return NotFound("Don't exist any authors");
            }

            return Ok(authors);
        }


        [HttpGet("search/{name}")]
        public ActionResult<IEnumerable<AuthorDTO>> SearchAuthorByName(string name)
        {
            var authors = _context.Authors
                .Where(a => a.Name.Contains(name))
                .Include(a => a.Books)
                .ToList()
                .Select(a => MapToDTO(a))
                .ToList();

            if (!authors.Any())
            {
                return NotFound($"Don't exist author with name : {name}");
            }

            return Ok(authors);
        }


        [HttpPost]
        public ActionResult<AuthorDTO> CreateAuthor([FromBody] AuthorCreateDTO authorCreateDTO)
        {
            if (authorCreateDTO == null)
            {
                return BadRequest("Data of author can't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                Name = authorCreateDTO.Name,
                DateOfBirth = authorCreateDTO.DateOfBirth
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, MapToDTO(author));
        }


        [HttpPut("{id}")]
        public ActionResult<AuthorDTO> UpdateAuthor(int id, [FromBody] AuthorCreateDTO updatedAuthorDTO)
        {
            if (updatedAuthorDTO == null)
            {
                return BadRequest("Data cant't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = _context.Authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound($"Don't exist author with id {id}");
            }

            author.Name = updatedAuthorDTO.Name;
            author.DateOfBirth = updatedAuthorDTO.DateOfBirth;

            _context.Update(author);
            _context.SaveChanges();

            return Ok(MapToDTO(author));
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound($"Don't exist author with id {id}");
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return NoContent();
        }

        
        private AuthorDTO MapToDTO(Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                DateOfBirth = author.DateOfBirth,
                Books = author.Books?.Select(b => new BookInAuthorDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishedYear = b.PublishedYear
                }).ToList() ?? new List<BookInAuthorDTO>()
            };
        }
    }
}
