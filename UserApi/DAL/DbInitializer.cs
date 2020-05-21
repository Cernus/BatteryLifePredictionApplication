using UserApi.Models;
using System.Linq;

namespace UserApi.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(BatteryUserContext context)
        {
            context.Database.EnsureCreated();

            // Add initial user data if database is empty
            if (!context.Users.Any())
            {
                User[] users = new User[]
                {
                    new User
                    {
                        Username = "admin",
                        Password = "password",
                        JobTitle = "Administrator",
                        EmailAddress = "admin@gmail.com",
                        Active = true
                    },
                    new User
                    {
                        Username = "joebloggs",
                        Password = "password",
                        JobTitle = "Assistant",
                        EmailAddress = "jbloggs@gmail.com",
                        Active = true
                    },
                };

                foreach (User b in users)
                {
                    context.Users.Add(b);
                }
            }

            context.SaveChanges();
        }
    }
}
