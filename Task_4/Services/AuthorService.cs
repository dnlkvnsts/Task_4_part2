using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Task_4.DTOs;
using Task_4.Extentions;
using Task_4.Models;
using Task_4.Repositories;
using System.Linq;

namespace Task_4.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStringLocalizer<AuthorService> _localizer;
        private readonly IMapperService _mapper;

        public AuthorService(
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IMapperService mapper,
            IStringLocalizer<AuthorService> localizer)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _localizer = localizer;
        }

        public ActionResult<IEnumerable<AuthorDTO>> GetAllAuthors()
        {
            var authors = _authorRepository.GetAll();

            return new OkObjectResult(authors.Select(_mapper.MapAuthorToDTO));
        }

        public ActionResult<AuthorDTO> GetAuthorById(int id)
        {
            var author = _authorRepository.GetById(id);

            if (author == null)
            {
                string localizedMessage = _localizer["AuthorNotFound", id];
                return ResultExtension.EntityNotFound<AuthorDTO>(id, "Author",localizedMessage);
            }


            return new OkObjectResult(_mapper.MapAuthorToDTO(author));
        }

        public ActionResult<AuthorDTO> CreateAuthor(AuthorCreateDTO dto)
        {
            var author = new Author
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth
            };

            _authorRepository.Create(author);


            return new CreatedResult(
                $"/api/authors/{author.Id}",
                _mapper.MapAuthorToDTO(author)
            );
        }

        public ActionResult<AuthorDTO> UpdateAuthor(int id, AuthorCreateDTO dto)
        {
            var author = _authorRepository.GetById(id);


            if (author == null)
            {
                string localizedMessage = _localizer["AuthorNotFound", id];
                return ResultExtension.EntityNotFound<AuthorDTO>(id, "Author",localizedMessage);
            }

            author.Name = dto.Name;
            author.DateOfBirth = dto.DateOfBirth;

            _authorRepository.Update(author);


            return new OkObjectResult(_mapper.MapAuthorToDTO(author));
        }

        public ActionResult<AuthorDTO> DeleteAuthor(int id)
        {
            var author = _authorRepository.GetById(id);



            if (author == null)
            {
                string localizedMessage = _localizer["AuthorNotFound", id];
                return ResultExtension.EntityNotFound<AuthorDTO>(id, "Author", localizedMessage);
            }


            var books = _bookRepository.GetBooksByAuthorId(id).ToList();

            if (books.Any())
            {
                string message = _localizer["CannotDeleteAuthorWithBooks", books.Count];
                return ResultExtension.EntityBadRequest(message); 
            }

            _authorRepository.Delete(id);

            return new NoContentResult();
        }
        public ActionResult<IEnumerable<object>> GetAllAuthorsWithBookCounts()
        {
            var authorsWithCounts = _authorRepository.GetAllAuthorsWithBookCount();
            return new OkObjectResult(authorsWithCounts);
        }

        public ActionResult<IEnumerable<AuthorDTO>> SearchAuthorsByName(string namePart)
        {
            var authors = _authorRepository.FindAuthorsByName(namePart).ToList();
            if (!authors.Any()) 
            {
                string localizedMessage = _localizer["NoAuthorsFoundForSearch", namePart];
                
                return ResultExtension.EntityNotFound<IEnumerable<AuthorDTO>>(0, "Authors", localizedMessage);
            }

            return new OkObjectResult(authors.Select(_mapper.MapAuthorToDTO));
        }
    }
}