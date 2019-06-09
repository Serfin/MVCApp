using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Common.ViewModels;
using MVCApp.Core.Enums;

namespace MVCApp.Infrastructure.Interfaces
{
    public interface IUserService : IService
    {
        Task RegisterAsync(Guid userId, string email, string ign,
            string password, SystemRole role);

        Task LoginAsync(string email, string password);
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByIdAsync(Guid userId);
        Task<UserViewModel> GetByIgnAsync(string ign);
        Task<UserViewModel> GetByEmailAsync(string email);
        Task UpdateAccountAsync(UserViewModel userViewModel);

        Task DeleteAccountAsync(Guid userId);
    }
}