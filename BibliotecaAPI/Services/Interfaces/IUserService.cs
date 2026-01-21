using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();

        Task<UserDto> GetUserById(int id);

        Task<UserDto> AddUser(UserDto dto);

        Task<UserDto> UpdateUser(int id, UserDto dto);

        Task<bool> DeleteUser(int id);
    }
}
