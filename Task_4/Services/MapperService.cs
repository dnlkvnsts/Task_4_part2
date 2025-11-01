using Task_4.DTOs;
using Task_4.Models;

namespace Task_4.Services
{
    public class MapperService : IMapperService
    {
        public AuthorDTO MapAuthorToDTO(Author author) => new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };

        public BookDTO MapBookToDTO(Book book) => new()
        {
            Id = book.Id,
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        };
    }
}