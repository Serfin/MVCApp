using System.Collections.Generic;
using System.Threading.Tasks;
using MVCApp.Infrastructure.ViewModels;

namespace MVCApp.Infrastructure.Services
{
    public interface IRotationService : IService
    {
        Task<IEnumerable<RotationViewModel>> GetAllAsync();
    }
}