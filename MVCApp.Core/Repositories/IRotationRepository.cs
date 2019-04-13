using System.Collections.Generic;
using System.Threading.Tasks;
using ExileRota.Core.Domain;

namespace MVCApp.Core.Repositories
{
    public interface IRotationRepository : IRepository
    {
        Task AddAsync(Rotation rotation);

        Task<IEnumerable<Rotation>> GetAllAsync();
    }
}