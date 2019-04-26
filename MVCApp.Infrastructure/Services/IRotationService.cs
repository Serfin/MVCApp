using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.DTO;

namespace MVCApp.Infrastructure.Services
{
    public interface IRotationService : IService
    {
        Task CreateRotationAsync(Guid rotationId, Guid userId, string league, RotationType type, int spots);

        Task<IEnumerable<RotationDto>> GetAllAsync();
    }
}