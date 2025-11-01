using Microsoft.AspNetCore.Mvc;
using Task_4.DTOs;

namespace Task_4.Services
{
    public interface IBookService
    {
        ActionResult<IEnumerable<BookDTO>> GetAllBooks();
        ActionResult<BookDTO> GetBookById(int id);
        ActionResult<BookDTO> CreateBook(BookCreateDTO dto);
        ActionResult<BookDTO> UpdateBook(int id, BookCreateDTO dto);
        ActionResult<BookDTO> DeleteBook(int id);

        ActionResult<IEnumerable<BookDTO>> GetBooksAfterYear();
    }
}
