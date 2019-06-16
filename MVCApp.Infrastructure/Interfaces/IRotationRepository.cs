using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Core.Domain;
using MVCApp.Core.Enums;

namespace MVCApp.Infrastructure.Interfaces
{
    public interface IRotationRepository : IRepository
    {
        Task AddAsync(Rotation rotation);

        Task<Rotation> GetByRotationId(Guid rotationId);
        Task<IEnumerable<Rotation>> GetByTypeAsync(RotationType type);
        Task<IEnumerable<Rotation>> GetByUserId(Guid userId);
        Task<IEnumerable<Rotation>> GetPageAsync(int page, int pageSize);
        Task<IEnumerable<User>> GetRotationMembersAsync(Guid rotationId);

        Task UpdateRotationAsync(Rotation rotation);

        Task DeleteRotationAsync(Guid rotationId);
    }
}