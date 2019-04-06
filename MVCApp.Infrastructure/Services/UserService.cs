using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MVCApp.Core.Domain;
using MVCApp.Core.Repositories;
using MVCApp.Infrastructure.ViewModels;

namespace MVCApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _mapper = mapper;
        }

        public async Task RegisterAsync(Guid userId, string ign, string email, string password, string role)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
            {
                throw new Exception($"User with email: {email} already exists.");
            }

            user = await _userRepository.GetByIgnAsync(ign);
            if (user != null)
            {
                throw new Exception($"User with username: {ign} already exists.");
            }

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            user = new User(userId, email, ign, hash, salt, role);
            await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetByIdAsync(Guid userId)
        {
            if (userId == null)
                throw new ArgumentNullException($"{nameof(userId)} cannot be null");

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException($"{nameof(user)} does not exist");

            return _mapper.Map<User, UserViewModel>(user);
        }

        public async Task UpdateUserAsync(UserViewModel userViewModel)
        {
            var user = await _userRepository.GetByIdAsync(userViewModel.UserId);
            var newUser = _mapper.Map<UserViewModel, User>(userViewModel, user);
            newUser.SetUpdateTime(DateTime.Now);
            await _userRepository.UpdateAsync(newUser);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            await _userRepository.DeleteAsync(user);
        }
    }
}