using BibliotecaAPI.Data;
using BibliotecaAPI.Repository;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BibliotecaAPI.Services
{
    public class BookService : IBookService
    {
        private IRepository<Books> _repository;
        public BookService(IRepository<Books> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var books = await _repository.Get();

            return books.Select(book => new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre
            });
        }

        public async Task<BookDto> GetBookById(int id)
        {
            var book = await _repository.GetById(id);

            if (book == null)
            {
                return null;
            }

            var bookDto = new BookDto // lo que se devolvera al usuario
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre
            };

            return bookDto;
        }

        public async Task<BookDto> AddBook(BookDto bookDto)
        {
            var bookinsert = new Books // lo que el usuario envia
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                PublishedYear = bookDto.PublishedYear,
                Genre = bookDto.Genre
            };

            await _repository.Create(bookinsert);
            await _repository.Save();

            var bookResult = new BookDto // lo que se mandara al header para ver para ver de donde se obtuvo
            {
                BookId = bookinsert.BookId,
                Title = bookinsert.Title,
                Author = bookinsert.Author,
                PublishedYear = bookinsert.PublishedYear,
                Genre = bookinsert.Genre
            };

            return bookResult;
        }

        public async Task<BookDto> UpdateBook(int id, BookDto bookDto)
        {
            var bookToUpdate = await _repository.GetById(id);

            if (bookToUpdate == null)
            {
                return null;
            }

            bookToUpdate.Title = bookDto.Title;
            bookToUpdate.Author = bookDto.Author;
            bookToUpdate.PublishedYear = bookDto.PublishedYear;
            bookToUpdate.Genre = bookDto.Genre;

            _repository.Update(bookToUpdate);
            await _repository.Save();

            var bookResult = new BookDto
            {
                BookId = bookToUpdate.BookId,
                Title = bookToUpdate.Title,
                Author = bookToUpdate.Author,
                PublishedYear = bookToUpdate.PublishedYear,
                Genre = bookToUpdate.Genre
            };

            return bookResult;
        }
        
        public async Task<bool> DeleteBook(int id)
        {
            var bookDelete = await _repository.GetById(id);

            if (bookDelete == null)
            {
                return false;
            }

            _repository.Delete(bookDelete);
            await _repository.Save();

            return true;
        }
            


    }
}
