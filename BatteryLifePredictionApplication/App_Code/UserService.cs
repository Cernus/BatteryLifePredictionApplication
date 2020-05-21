using AppFacade;
using AppFacade.Models;
using System.Collections.Generic;

namespace BatteryLifePredictionApplication.App_Code
{
    // Handles all the application's functionality associated with USers
    public static class UserService
    {
        // Create a User
        public static bool CreateUser(UserDto user)
        {
            Facade facade = new Facade();
            bool result = facade.CreateUser(user);
            return result;
        }

        // Get all Users
        public static List<UserDto> GetUsers()
        {
            Facade facade = new Facade();
            List<UserDto> users = facade.GetUsers();
            return users;
        }

        // Get a User
        public static UserDto GetUser(int id)
        {
            Facade facade = new Facade();
            UserDto user = facade.GetUser(id);
            return user;
        }

        // Update a User
        public static bool UpdateUser(UserDto user)
        {
            Facade facade = new Facade();
            bool result = facade.UpdateUser(user);
            return result;
        }

        // Soft-delete a User
        public static bool DeleteUser(int id)
        {
            Facade facade = new Facade();
            bool result = facade.DeleteUser(id);
            bool isAdmin = Security.IsAdmin();

            if(result && !isAdmin)
            {
                Security.SignOut();
            }

            return result;
        }

        // Check if a specific username has already been taken by an existing User
        public static bool? UsernameExists(string username)
        {
            Facade facade = new Facade();
            bool? result = facade.UsernameExists(username);
            return result;
        }
    }
}