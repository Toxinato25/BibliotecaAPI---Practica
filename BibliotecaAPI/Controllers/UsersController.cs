using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services.Interfaces;
using BibliotecaAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private IValidator<UserDto> _validator;

        public UsersController(IUserService userService,
            IValidator<UserDto> _validator)
        {
            _userService = userService;
            this._validator = _validator;
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
            var validator = await _validator.ValidateAsync(dto);
            
            if (!validator.IsValid)
                return BadRequest(validator.Errors);

            var result = await _userService.AddUser(dto);

            if (result == null)
                return BadRequest("No se pudo agregar el usuario");

            return CreatedAtAction(nameof(GetUserById), new { id = result.UserId}, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto)
        {
            var validators = await _validator.ValidateAsync(dto);

            if (!validators.IsValid)
                return BadRequest(validators.Errors);

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
