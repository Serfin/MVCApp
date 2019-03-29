using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using MVCApp.Core.Domain;
using MVCApp.Core.Repositories;
using MVCApp.Data.EntityFramework;

namespace MVCApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MVCAppContext _context;

        public UserRepository(MVCAppContext context)
        {
            _context = context;
        }

        public Task<User> GetByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIgnAsync(string ign)
        {
            throw new NotImplementedException();
        }

        // TODO : Add pagination
        public async Task<IEnumerable<User>> GetAllAsync()
            => await _context.Users.ToListAsync();

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}