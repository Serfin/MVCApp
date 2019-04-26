using System;
using System.Threading.Tasks;
using AutoMapper;
using MVCApp.Core.Repositories;

namespace MVCApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new ArgumentException("Invalid credentials");

            var hash = _encrypter.GetHash(password, user.Salt);

            if (user.Password != hash)
                throw new ArgumentException("Invalid credentials");
        }
    }
}