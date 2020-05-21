using UserApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(UserDto user);
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUser(int id);
        Task<User> UpdateUser(UserDto user);
        Task<User> DeleteUser(int id);
        Task<UserDto> GetUser(string username, string password);
        Task<bool> IsActive(int id);
        Task<bool?> UsernameExists(string username);
    }
}
