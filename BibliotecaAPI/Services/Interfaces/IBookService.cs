using System.Collections.Generic;
using BibliotecaAPI.Models;
using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooks();

        Task<BookDto> GetBookById(int id);

        Task<BookDto> AddBook(BookDto dto);

        Task<BookDto> UpdateBook(int id, BookDto dto);

        Task<bool> DeleteBook(int id);
    }
}
