using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Infrastructure.DTO;

namespace MVCApp.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task RegisterAsync(Guid userId, string email, string ign, 
            string password, string role);

        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid userId);
        Task UpdateUserAsync(UserDto userViewModel);
        Task LoginAsync(string email, string password);

        Task DeleteUserAsync(Guid userId);
    }
}