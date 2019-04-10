using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Infrastructure.DTO;

namespace MVCApp.Infrastructure.Services
{
    public interface IRotationService : IService
    {
        Task<IEnumerable<RotationDto>> GetAllAsync();
    }
}