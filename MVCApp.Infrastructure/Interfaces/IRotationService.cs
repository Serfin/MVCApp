using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Common.ViewModels;
using MVCApp.Core.Enums;

namespace MVCApp.Infrastructure.Interfaces
{
    public interface IRotationService : IService
    {
        Task CreateRotationAsync(Guid rotationId, Guid userId, string creatorIgn, LeagueName league, RotationType type, int spots);

        Task<IEnumerable<RotationViewModel>> GetPageAsync(int page, int pageSize);

        Task JoinRotationAsync(Guid userId, Guid rotationId);
        Task LeaveRotationAsync(Guid userId, Guid rotationId);

        Task DeleteRotation(Guid rotationId);
    }
}