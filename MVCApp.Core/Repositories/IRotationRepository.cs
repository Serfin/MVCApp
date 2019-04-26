using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Core.Domain;
using MVCApp.Core.Enums;

namespace MVCApp.Core.Repositories
{
    public interface IRotationRepository : IRepository
    {
        Task AddAsync(Rotation rotation);

        Task<IEnumerable<Rotation>> GetByType(RotationType type);
        Task<IEnumerable<Rotation>> GetAllAsync();
    }
}