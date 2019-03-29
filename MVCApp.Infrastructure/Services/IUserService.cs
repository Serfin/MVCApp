using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Infrastructure.ViewModels;

namespace MVCApp.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task RegisterAsync(Guid userId, string email, string ign, 
            string password, string role);

        Task<IEnumerable<UserViewModel>> GetAllAsync();
    }
}