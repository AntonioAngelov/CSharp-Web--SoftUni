namespace WebServer.Application.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using ViewModels.Account;

    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using (var context = new ByTheCakeDbContext())
            {
                if (context.Users.Any(u => u.Username == username))
                {
                    return false;
                }

                var user = new User
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.UtcNow
                };

                context.Users.Add(user);
                context.SaveChanges();

                return true;
            }
        }

        public bool Find(string username, string password)
        {
            using (var context = new ByTheCakeDbContext())
            {
                return context.Users
                    .Any(u => u.Username == username && u.Password == password);
            }
        }

        public ProfileViewModel Profile(string username)
        {
            using (var context = new ByTheCakeDbContext())
            {
                return context
                    .Users
                    .Where(u => u.Username == username)
                    .Select(u => new ProfileViewModel
                    {
                        Username = u.Username,
                        RegistrationDate = u.RegistrationDate,
                        TotalOrders = u.Orders.Count
                    })
                    .FirstOrDefault();
            }
        }

        public int? GetUserId(string username)
        {
            using (var context = new ByTheCakeDbContext())
            {
                var id = context.Users
                    .Where(u => u.Username == username)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                return id != 0 ? (int?)id : null;
            }
        }
    }
}
