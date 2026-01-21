using Microsoft.AspNetCore.Http;
using BibliotecaAPI.Services;
using BibliotecaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BibliotecaAPI.DTOs;
using Microsoft.JSInterop.Infrastructure;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks() => Ok(await _bookService.GetAllBooks());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var bookresult = await _bookService.GetBookById(id);
            if (bookresult == null)
                return NotFound("Libro no encontrado");

            return Ok(bookresult);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDto dto)
        {
            if (dto == null)
                return BadRequest("Datos del libro no proporcionados");

            var result = await _bookService.AddBook(dto);

            if (result == null)
                return BadRequest("No se pudo agregar el libro");

            return CreatedAtAction(nameof(GetBookById), new {id = result.BookId }, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto dto)
        {
            if (dto == null || id != dto.BookId)
                return BadRequest("Datos del libro invalidos");

            var result = await _bookService.UpdateBook(id, dto);

            if (result == null)
                return NotFound("No se pudo encontrar el libro para actualizar");

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _bookService.DeleteBook(id);

            if (!deleted)
                return NotFound("No se pudo encontrar el libro para eliminar");

            return NoContent();
        }
        

    }
}
