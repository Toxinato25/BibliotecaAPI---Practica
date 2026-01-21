using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult>  GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto dto)
        {
            if (dto == null)
                return BadRequest("Datos de usuario invalido");

            var result = await _userService.AddUser(dto);

            if (result == null)
                return BadRequest("No se pudo agregar el usuario");

            return CreatedAtAction(nameof(GetUserById), new { id = result.UserId}, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto)
        {
            if (dto == null || id != dto.UserId)
                return BadRequest("Datos de usuario invalido");

            var result = await _userService.UpdateUser(id, dto);

            if (result == null)
                return NotFound("Usuario no encontrado");

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUser(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
