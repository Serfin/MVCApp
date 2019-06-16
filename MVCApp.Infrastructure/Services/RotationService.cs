using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MVCApp.Common.Exceptions;
using MVCApp.Common.ViewModels;
using MVCApp.Core.Domain;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Infrastructure.Services
{
    public class RotationService : IRotationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRotationRepository _rotationRepository;
        private readonly IMapper _mapper;

        public RotationService(IUserRepository userRepository, IRotationRepository rotationRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _rotationRepository = rotationRepository;
            _mapper = mapper;
        }

        public async Task CreateRotationAsync(Guid rotationId, Guid userId, string creatorIgn, LeagueName league, RotationType type, int spots)
        {
            var rotation = new Rotation(rotationId, userId, creatorIgn, league, type, spots);

            await _rotationRepository.AddAsync(rotation);
        }

        public async Task<IEnumerable<RotationViewModel>> GetPageAsync(int page, int pageSize)
        {
            try
            {
                if (page < 1 || pageSize % 12 != 0)
                {
                    throw new InvalidPaginationArgument();
                }

                var rotations = await _rotationRepository.GetPageAsync(page, pageSize);

                return _mapper.Map<IEnumerable<Rotation>, IEnumerable<RotationViewModel>>(rotations);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserViewModel>> GetRotationMembersAsync(Guid rotationId)
        {
            var members = await _rotationRepository.GetRotationMembersAsync(rotationId);

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(members);
        }

        public async Task JoinRotationAsync(Guid userId, Guid rotationId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var rotation = await _rotationRepository.GetByRotationId(rotationId);

            rotation.AddMember(user);
            await _rotationRepository.UpdateRotationAsync(rotation);
        }

        public async Task LeaveRotationAsync(Guid userId, Guid rotationId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var rotation = await _rotationRepository.GetByRotationId(rotationId);

            // TODO : Check if user that want to leave is user that is logged in
            rotation.DeleteMember(user);
            await _rotationRepository.UpdateRotationAsync(rotation);
        }

        public async Task DeleteRotation(Guid userId, Guid rotationId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var rotation = await _rotationRepository.GetByRotationId(rotationId);

            if (user == null)
                throw new ArgumentNullException($"{nameof(user)} cannot be null");

            if (rotation == null)
                throw new ArgumentNullException($"{nameof(rotation)} cannot be null");

            if (rotation.Creator == user.UserId)
                await _rotationRepository.DeleteRotationAsync(rotationId);
            else
                throw new Exception($"{nameof(user)} is not the owner of rotation {nameof(rotation)}");
        }
    }
}