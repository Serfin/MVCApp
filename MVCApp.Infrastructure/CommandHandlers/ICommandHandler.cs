using System.Threading.Tasks;
using MVCApp.Infrastructure.Commands;

namespace MVCApp.Infrastructure.CommandHandlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}