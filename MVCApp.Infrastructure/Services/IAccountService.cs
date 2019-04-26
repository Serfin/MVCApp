using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.DTO;

namespace MVCApp.Infrastructure.Services
{
    public interface IAccountService
    {
        Task RegisterAsync(Guid userId, string email, string ign,
            string password, SystemRole role);

        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid userId);
        Task UpdateAccountAsync(UserDto userViewModel);

        Task DeleteAccountAsync(Guid userId);
    }
}