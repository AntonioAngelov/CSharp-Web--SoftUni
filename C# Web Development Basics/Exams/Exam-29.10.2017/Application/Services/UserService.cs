namespace Application.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly JudgeDbContext db;

        public UserService(JudgeDbContext db)
        {
            this.db = db;
        }

        public bool Create(string email, string password, string fullName)
        {
            if (this.db.Users.Any(u => u.Email == email))
            {
                return false;
            }

            var isAdmin = !this.db.Users.Any();

            var user = new User
            {
                Email = email,
                Password = password,
                FullName = fullName,
                IsAdmin = isAdmin
                
            };

            this.db.Add(user);
            this.db.SaveChanges();

            return true;
        }

        public bool UserExists(string email, string password)
            => this.db
                .Users
                .Any(u => u.Email == email && u.Password == password);
        
       
    }
}
