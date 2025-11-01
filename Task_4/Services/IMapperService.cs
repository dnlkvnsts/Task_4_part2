using Task_4.DTOs;
using Task_4.Models;

namespace Task_4.Services
{
    public interface IMapperService
    {
        AuthorDTO MapAuthorToDTO(Author author);
        BookDTO MapBookToDTO(Book book);
    }
}