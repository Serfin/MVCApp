﻿using System;
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
        Task<UserViewModel> GetByIdAsync(Guid userId);
        Task UpdateUserAsync(UserViewModel userViewModel);
        Task LoginAsync(string email, string password);

        Task DeleteUserAsync(Guid userId);
    }
}