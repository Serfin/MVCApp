using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Core.Domain;

namespace MVCApp.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetByIdAsync(Guid userId);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIgnAsync(string ign);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}