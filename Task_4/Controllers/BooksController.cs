using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_4.Data;
using Task_4.DTOs;
using Task_4.Models;
using Task_4.Services;

namespace Task_4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;



        public BooksController(
            IBookService service)

        {
            _service = service;
        }


        [HttpGet]

        public ActionResult<IEnumerable<BookDTO>> GetAll()
        {

            return _service.GetAllBooks();
        }

        [HttpGet("{id}")]

        public ActionResult<BookDTO> GetById(int id)
        {

            return _service.GetBookById(id);
        }


        [HttpPost]

        public async Task<ActionResult<BookDTO>> Create([FromBody] BookCreateDTO dto)
        {

            var bookResult = _service.CreateBook(dto);

            if (bookResult.Result is CreatedResult createdResult && createdResult.Value is BookDTO book)
            {
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            return bookResult;
        }


        [HttpPut("{id}")]

        public async Task<ActionResult<BookDTO>> Update(int id, [FromBody] BookCreateDTO dto)
        {


            return _service.UpdateBook(id, dto);
        }


        [HttpDelete("{id}")]

        public ActionResult<BookDTO> Delete(int id)
        {

            _service.DeleteBook(id);
            return NoContent();
        }


        [HttpGet("publishedAfter2015")]
        public ActionResult<IEnumerable<BookDTO>> GetBooksPublishedAfter()
        {
            return _service.GetBooksAfterYear();
        }
    }
}
