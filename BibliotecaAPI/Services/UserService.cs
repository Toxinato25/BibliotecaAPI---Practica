using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BibliotecaAPI.Services
{
    public class UserService : IUserService
    {
        private readonly BibliotecaContext _context;
        public UserService(BibliotecaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsers() =>
            await _context.Users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.Username,
                Email = u.Email,
                RegisteredAt = u.RegisteredAt

            }).ToListAsync();

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.Username,
                Email = user.Email,
                RegisteredAt = user.RegisteredAt
            };

            return userDto;
        }

        public async Task<UserDto> AddUser(UserDto dto)
        {
            var userInsert = new Users // Lo que el user ingresa
            {
                Username = dto.UserName,
                Email = dto.Email,
            };

            await _context.Users.AddAsync(userInsert);
            await _context.SaveChangesAsync();

            var userResult = new UserDto // lo que devuelve al header 
            {
                UserId = userInsert.UserId,
                UserName = userInsert.Username,
                Email = userInsert.Email,
                RegisteredAt = userInsert.RegisteredAt
            };

            return userResult;
        }

        public async Task<UserDto> UpdateUser(int id, UserDto dto)
        {
            var userToUpdate = await _context.Users.FindAsync(id);

            if(userToUpdate == null)
                return null;

            userToUpdate.Username = dto.UserName;
            userToUpdate.Email = dto.Email;
            
            await _context.SaveChangesAsync();

            var userResult = new UserDto // lo que devuelve al header
            {
                UserId = userToUpdate.UserId,
                UserName = userToUpdate.Username,
                Email = userToUpdate.Email,
                RegisteredAt = userToUpdate.RegisteredAt
            };

            return userResult;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);

            if (userToDelete == null)
                return false;
            
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
        
    }
}
