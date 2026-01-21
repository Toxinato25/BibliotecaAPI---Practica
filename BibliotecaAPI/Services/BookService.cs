using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BibliotecaAPI.Services
{
    public class BookService : IBookService
    {
        private readonly BibliotecaContext _context; 
        public BookService(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks() => 
            await _context.Books.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Author = b.Author,
                PublishedYear = b.PublishedYear,
                Genre = b.Genre
            }).ToListAsync();

        public async Task<BookDto> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);

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

            await _context.Books.AddAsync(bookinsert);
            await _context.SaveChangesAsync();

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
            var bookToUpdate = await _context.Books.FindAsync(id);

            if (bookToUpdate == null)
            {
                return null;
            }

            bookToUpdate.Title = bookDto.Title;
            bookToUpdate.Author = bookDto.Author;
            bookToUpdate.PublishedYear = bookDto.PublishedYear;
            bookToUpdate.Genre = bookDto.Genre;

            await _context.SaveChangesAsync();

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
            var bookDelete = await _context.Books.FindAsync(id);

            if (bookDelete == null)
            {
                return false;
            }

            _context.Books.Remove(bookDelete);
            await _context.SaveChangesAsync();

            return true;
        }
            


    }
}
