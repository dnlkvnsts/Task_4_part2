using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_4.Data;
using Task_4.Models;
using Task_4.DTOs.Book;  

namespace Task_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetAllBooksWithAuthor()
        {
            var books = _context.Books
                .Include(b => b.Author)
                .ToList()
                .Select(b => MapToDTO(b))  
                .ToList();

            return Ok(books);
        }


        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetBookById(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Don't exist book with id {id}");
            }

            return Ok(MapToDTO(book));  
        }


        [HttpGet("get-books-published-after-2015")]
        public ActionResult<IEnumerable<BookDTO>> GetBooksAfter2015()
        {
            var books = _context.Books
                .Include(b => b.Author)
                .Where(b => b.PublishedYear > 2015)
                .ToList()
                .Select(b => MapToDTO(b)) 
                .ToList();

            if (!books.Any())
            {
                return NotFound("No books published after 2015");
            }

            return Ok(books);
        }


        [HttpPost]
        public ActionResult<BookDTO> CreateBook([FromBody] BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO == null)
            {
                return BadRequest("Data of book can't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorExists = _context.Authors.Any(a => a.Id == bookCreateDTO.AuthorId);

            if (!authorExists)
            {
                return BadRequest($"Don't exist author with id {bookCreateDTO.AuthorId}");
            }

            var book = new Book
            {
                Title = bookCreateDTO.Title,
                PublishedYear = bookCreateDTO.PublishedYear,
                AuthorId = bookCreateDTO.AuthorId
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, MapToDTO(book));
        }


        [HttpPut("{id}")]
        public ActionResult<BookDTO> UpdateBook(int id, [FromBody] BookCreateDTO updatedBookDTO)
        {
            if (updatedBookDTO == null)
            {
                return BadRequest("Data cant't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Don't exist book with id {id}");
            }

            if (updatedBookDTO.AuthorId != book.AuthorId)
            {
                var author = _context.Authors
                    .FirstOrDefault(a => a.Id == updatedBookDTO.AuthorId);
                if (author == null)
                {
                    return BadRequest($"Don't exist author with id {updatedBookDTO.AuthorId}");
                }
            }

            book.Title = updatedBookDTO.Title;
            book.PublishedYear = updatedBookDTO.PublishedYear;
            book.AuthorId = updatedBookDTO.AuthorId;

            _context.Books.Update(book);
            _context.SaveChanges();

            return Ok(MapToDTO(book)); 
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Don't exist book with id {id}");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return NoContent();
        }

        private BookDTO MapToDTO(Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                Author = book.Author != null ? new AuthorInBookDTO
                {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                    DateOfBirth = book.Author.DateOfBirth
                 
                } : null
            };
        }
    }
}
