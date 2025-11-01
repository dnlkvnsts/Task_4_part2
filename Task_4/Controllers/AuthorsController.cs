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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(
            IAuthorService service)
        {
            _service = service;
        }


        [HttpGet]

        public ActionResult<IEnumerable<AuthorDTO>> GetAll()
        {
            return _service.GetAllAuthors();
        }


        [HttpGet("{id}")]

        public ActionResult<AuthorDTO> GetById(int id)
        {

            return _service.GetAuthorById(id);
        }


        [HttpPost]

        public async Task<ActionResult<AuthorDTO>> Create([FromBody] AuthorCreateDTO dto)
        {
            var authorResult = _service.CreateAuthor(dto);
           
            if (authorResult.Result is CreatedResult createdResult && createdResult.Value is AuthorDTO author)
            {
                return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
            }
            return authorResult; 

        }


        [HttpPut("{id}")]

        public async Task<ActionResult<AuthorDTO>> Update(int id, [FromBody] AuthorCreateDTO dto)
        {
            return _service.UpdateAuthor(id, dto);
        }


        [HttpDelete("{id}")]

        public ActionResult<AuthorDTO> Delete(int id)
        {

            return _service.DeleteAuthor(id);
        }

        [HttpGet("withNumberOfBooks")]
        public ActionResult<IEnumerable<object>> GetAuthorsWithBookCounts()
        {
            return _service.GetAllAuthorsWithBookCounts();
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<AuthorDTO>> SearchAuthors([FromQuery] string namePart)
        {
            return _service.SearchAuthorsByName(namePart);
        }
    }
}
