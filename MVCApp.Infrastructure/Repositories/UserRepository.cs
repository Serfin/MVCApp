using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCApp.Core.Domain;
using MVCApp.Core.Repositories;

namespace MVCApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        // TODO : Add EF support
        private static ISet<User> _users = new HashSet<User>()
        {
            new User(Guid.NewGuid(), "email", "ign", "password", "salt", "role")
        };

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

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
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