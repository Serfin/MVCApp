using System.Threading.Tasks;

namespace MVCApp.Infrastructure.Interfaces
{
    public interface IUserService : IService
    {
        Task LoginAsync(string email, string password);
    }
}