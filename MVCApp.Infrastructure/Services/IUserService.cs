using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.DTO;

namespace MVCApp.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task LoginAsync(string email, string password);
    }
}