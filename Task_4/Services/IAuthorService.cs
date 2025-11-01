using Microsoft.AspNetCore.Mvc;
using Task_4.DTOs;

namespace Task_4.Services
{
    public interface IAuthorService
    {
        ActionResult<IEnumerable<AuthorDTO>> GetAllAuthors();
        ActionResult<AuthorDTO> GetAuthorById(int id);
        ActionResult<AuthorDTO> CreateAuthor(AuthorCreateDTO dto);
        ActionResult<AuthorDTO> UpdateAuthor(int id, AuthorCreateDTO dto);
        ActionResult<AuthorDTO> DeleteAuthor(int id);
        ActionResult<IEnumerable<object>> GetAllAuthorsWithBookCounts(); 
        ActionResult<IEnumerable<AuthorDTO>> SearchAuthorsByName(string namePart); 
    }
}
