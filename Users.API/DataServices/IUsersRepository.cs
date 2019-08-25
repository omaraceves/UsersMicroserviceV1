using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Entities;

namespace Users.API.DataServices
{
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(Guid id);

        void AddUser(User userToAdd);

        Task<bool> SaveChangesAsync();

        Task<User> Authenticate(string userEmail, string password);
    }
}
