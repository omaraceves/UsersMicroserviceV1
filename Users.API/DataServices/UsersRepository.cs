using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Entities;
using Users.API.Helpers;

namespace Users.API.DataServices
{
    public class UsersRepository : IUsersRepository
    {
        private UsersContext _context;

        public UsersRepository(UsersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id) ?? null;
        }

        public void AddUser(User userToAdd)
        {
            _context.Users.Add(userToAdd);
        }

        public async Task<User> Authenticate(string userEmail, string password)
        {
            return await _context.Users.Where(x => x.Email == userEmail && x.Password == password)
                .FirstOrDefaultAsync() ?? null;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync() > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
