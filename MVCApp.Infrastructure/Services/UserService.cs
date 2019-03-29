using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Core.Domain;
using MVCApp.Core.Repositories;
using MVCApp.Infrastructure.Repositories;

namespace MVCApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // TODO: Add encrypter
        public async Task RegisterAsync(Guid userId, string ign, string email, string password, string role)
        {
            var user = new User(userId, email, ign, password, "salt", role);
            await _userRepository.AddAsync(user);
        }

        // TODO : Add object mapping
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }
    }
}