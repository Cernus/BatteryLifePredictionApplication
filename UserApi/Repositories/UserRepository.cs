using UserApi.DAL;
using UserApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BatteryUserContext _context;
        public UserRepository(BatteryUserContext context)
        {
            _context = context;
        }

        // Create a User in the database
        public async Task<User> CreateUser(UserDto user)
        {
            try
            {
                User entity = new User
                {
                    Username = user.Username,
                    Password = user.Password,
                    JobTitle = user.JobTitle,
                    EmailAddress = user.EmailAddress,
                    Active = true
                };

                _context.Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        // Get all Users in the database
        public async Task<List<UserDto>> GetUsers()
        {
            try
            {
                List<User> users = await _context.Users
                    .Where(u => u.Active == true)
                    .ToListAsync();

                List<UserDto> usersDto = users.Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Password = u.Password,
                    JobTitle = u.JobTitle,
                    EmailAddress = u.EmailAddress,
                }).ToList();

                return usersDto;
            }
            catch
            {
                return null;
            }
        }

        // Get a User in the database
        public async Task<UserDto> GetUser(int id)
        {
            try
            {
                User user = await _context.Users
                    .Where(u => u.UserId == id && u.Active == true)
                    .FirstOrDefaultAsync();

                UserDto userDto = new UserDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = user.Password,
                    JobTitle = user.JobTitle,
                    EmailAddress = user.EmailAddress
                };

                return userDto;
            }
            catch
            {
                return null;
            }
        }

        // Check that a User in the database has a specific username and password
        public async Task<UserDto> GetUser(string username, string password)
        {
            try
            {
                User entity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                UserDto userDto = new UserDto
                {
                    UserId = entity.UserId,
                    Username = entity.Username,
                    Password = entity.Password,
                    JobTitle = entity.JobTitle,
                    EmailAddress = entity.EmailAddress
                };

                return userDto;
            }
            catch
            {
                return null;
            }
        }

        // Update a User in the database
        public async Task<User> UpdateUser(UserDto user)
        {
            try
            {
                User entity = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);

                entity.Username = user.Username;
                entity.Password = user.Password;
                entity.JobTitle = user.JobTitle;
                entity.EmailAddress = user.EmailAddress;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        // Delete a User in the database
        public async Task<User> DeleteUser(int id)
        {
            try
            {
                // Prevent Admin from being deleted
                if(id == 1)
                {
                    return null;
                }

                User entity = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

                entity.Active = false;

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        // Check if a User has been soft-deleted
        public async Task<bool> IsActive(int id)
        {
            try
            {
                User user = await _context.Users
                    .Where(u => u.UserId == id)
                    .FirstOrDefaultAsync();

                return user.Active;
            }
            catch
            {
                return false;
            }
        }

        // Check if a User in the database has a specific username
        public async Task<bool?> UsernameExists(string username)
        {
            try
            {
                User entity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

                if(entity != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
