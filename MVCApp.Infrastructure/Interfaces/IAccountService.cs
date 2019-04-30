using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Common.ViewModels;
using MVCApp.Core.Enums;

namespace MVCApp.Infrastructure.Interfaces
{
    public interface IAccountService : IService
    {
        Task RegisterAsync(Guid userId, string email, string ign,
            string password, SystemRole role);

        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByIdAsync(Guid userId);
        Task UpdateAccountAsync(UserViewModel userViewModel);

        Task DeleteAccountAsync(Guid userId);
    }
}