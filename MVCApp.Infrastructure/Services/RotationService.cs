using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExileRota.Core.Domain;
using MVCApp.Core.Repositories;
using MVCApp.Infrastructure.ViewModels;

namespace MVCApp.Infrastructure.Services
{
    public class RotationService : IRotationService
    {
        private readonly IRotationRepository _rotationRepository;
        private readonly IMapper _mapper;

        public RotationService(IRotationRepository rotationRepository, IMapper mapper)
        {
            _rotationRepository = rotationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RotationViewModel>> GetAllAsync()
        {
            var rotations = await _rotationRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Rotation>, IEnumerable<RotationViewModel>>(rotations);
        }
    }
}