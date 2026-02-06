using Shop.Helpers;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Repositories
{
    public class UserRepository
    {
        private List<User> users;
        private int idCounter = 1;

        public UserRepository()
        {
            users = FileService.LoadUsers();
            if (users.Any())
                idCounter = users.Max(u => u.Id) + 1;

            if (!users.Any())
            {
                AddUser(new Seller { FirstName = "Admin", Email = "admin@shop.pl", Password = "admin", City = "System" });
            }
        }

        public void AddUser(User user)
        {
            user.Id = idCounter++;
            users.Add(user);
            FileService.SaveUsers(users);
        }

        public IEnumerable<User> GetAllUsers() => users;

        public User GetUserByEmail(string email) => users.FirstOrDefault(x => x.Email == email);

        // Metoda do usuwania
        public bool RemoveUserByEmail(string email)
        {
            var user = users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                users.Remove(user);
                FileService.SaveUsers(users);
                return true;
            }
            return false;
        }
    }
}