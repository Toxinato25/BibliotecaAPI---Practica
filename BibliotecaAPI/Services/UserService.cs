using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Repository;
using BibliotecaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BibliotecaAPI.Services
{
    public class UserService : IUserService
    {
        private IRepository<Users> _UsersRepository;
        public UserService(IRepository<Users> _UsersRepository)
        {
            this._UsersRepository = _UsersRepository;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _UsersRepository.Get();

            return users.Select(user => new UserDto
            {
                UserId = user.UserId,
                UserName = user.Username,
                Email = user.Email,
                RegisteredAt = user.RegisteredAt
            }).ToList();
        }
            

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _UsersRepository.GetById(id);

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

            userInsert.RegisteredAt = DateTime.Now;

            await _UsersRepository.Create(userInsert);
            await _UsersRepository.Save();

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
            var userToUpdate = await _UsersRepository.GetById(id);

            if(userToUpdate == null)
                return null;

            userToUpdate.Username = dto.UserName;
            userToUpdate.Email = dto.Email;
            
            await _UsersRepository.Save();

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
            var userToDelete = await _UsersRepository.GetById(id);

            if (userToDelete == null)
                return false;

            _UsersRepository.Delete(userToDelete);
            await _UsersRepository.Save();

            return true;
        }
        
    }
}
