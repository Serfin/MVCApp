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

        Task<Rotation> GetById(Guid rotationId);
        Task<IEnumerable<Rotation>> GetByType(RotationType type);
        Task<IEnumerable<Rotation>> GetByCreator(Guid userId);
        Task<IEnumerable<Rotation>> GetPageAsync(int page, int pageSize);

        Task UpdateRotationAsync(Rotation rotation);

        Task DeleteRotationAsync(Guid rotationId);
    }
}