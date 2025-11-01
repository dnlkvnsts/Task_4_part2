using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Task_4.DTOs;
using Task_4.Extentions;
using Task_4.Models;
using Task_4.Repositories;
using static System.Reflection.Metadata.BlobBuilder;

namespace Task_4.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapperService _mapper;
        private readonly IStringLocalizer<BookService> _stringLocalizer;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IMapperService mapper,
            IStringLocalizer<BookService> stringLocalizer)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public ActionResult<IEnumerable<BookDTO>> GetAllBooks()
        {
            var books = _bookRepository.GetAll();

            return new OkObjectResult(books.Select(_mapper.MapBookToDTO));
        }

        public ActionResult<BookDTO> GetBookById(int id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
            {
                string localizedMessage = _stringLocalizer["BookNotFound", id];
                return ResultExtension.EntityNotFound<BookDTO>(id, "Book",localizedMessage);
            }

            return new OkObjectResult(_mapper.MapBookToDTO(book));
        }

        public ActionResult<BookDTO> CreateBook(BookCreateDTO dto)
        {
            var author = _authorRepository.GetById(dto.AuthorId);

            if (author == null)
            {
                string localizedMessage = _stringLocalizer["AuthorDoesNotExist", dto.AuthorId];
                return ResultExtension.EntityBadRequest(localizedMessage);
            }

            var book = new Book
            {
                Title = dto.Title,
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };

            _bookRepository.Create(book);


            return new CreatedResult(
                $"/api/books/{book.Id}",
                _mapper.MapBookToDTO(book)
            );
        }

        public ActionResult<BookDTO> UpdateBook(int id, BookCreateDTO dto)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
            {
                string localizedMessage = _stringLocalizer["BookNotFound", id];
                return ResultExtension.EntityNotFound<BookDTO>(id, "Book", localizedMessage);
            }

            var author = _authorRepository.GetById(dto.AuthorId);

            if (author == null)
            {
                string localizedMessage = _stringLocalizer["AuthorDoesNotExist", dto.AuthorId];
                return ResultExtension.EntityBadRequest(localizedMessage);
            }

            book.Title = dto.Title;
            book.PublishedYear = dto.PublishedYear;
            book.AuthorId = dto.AuthorId;

            _bookRepository.Update(book);


            return new OkObjectResult(_mapper.MapBookToDTO(book));
        }

        public ActionResult<BookDTO> DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
            {
                string localizedMessage = _stringLocalizer["BookNotFound", id];
                return ResultExtension.EntityNotFound<BookDTO>(id, "Book", localizedMessage);
            }


            _bookRepository.Delete(id);

            return new NoContentResult();
        }

        public ActionResult<IEnumerable<BookDTO>> GetBooksAfterYear()
        {
            var books = _bookRepository.GetBooksPublishedAfterYear();
            return new OkObjectResult(books.Select(_mapper.MapBookToDTO));
        }
    }
}