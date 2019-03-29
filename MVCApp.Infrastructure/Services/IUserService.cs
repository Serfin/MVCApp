using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Core.Domain;

namespace MVCApp.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task RegisterAsync(Guid userId, string email, string ign, 
            string password, string role);

        Task<IEnumerable<User>> GetAllAsync();
    }
}