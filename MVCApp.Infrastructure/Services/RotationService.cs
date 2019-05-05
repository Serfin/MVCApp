using System;
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

        public async Task CreateRotationAsync(Guid rotationId, Guid userId, LeagueName league, RotationType type, int spots)
        {
            var rotation = new Rotation(rotationId, userId, league, type, spots);

            await _rotationRepository.AddAsync(rotation);
        }

        public async Task<IEnumerable<RotationViewModel>> GetPageAsync(int page, int pageSize)
        {
            if (page < 1 || pageSize % 12 != 0)
            {
                throw new InvalidPaginationArgument();
            }

            var rotations = await _rotationRepository.GetPageAsync(page, pageSize);

            return _mapper.Map<IEnumerable<Rotation>, IEnumerable<RotationViewModel>>(rotations);
        }

        public async Task JoinRotationAsync(Guid userId, Guid rotationId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var rotation = await _rotationRepository.GetById(rotationId);

            rotation.AddMember(user);
            await _rotationRepository.UpdateRotationAsync(rotation);
        }

        public async Task LeaveRotationAsync(Guid userId, Guid rotationId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var rotation = await _rotationRepository.GetById(rotationId);

            rotation.DeleteMember(user);
            await _rotationRepository.UpdateRotationAsync(rotation);
        }
        // TODO : Check if user is owner of rotation that is passed to delete !!!
        public async Task DeleteRotation(Guid rotationId)
            => await _rotationRepository.DeleteRotationAsync(rotationId);
    }
}