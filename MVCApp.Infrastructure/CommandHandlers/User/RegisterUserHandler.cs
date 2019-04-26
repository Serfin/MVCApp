using System;
using System.Threading.Tasks;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.Commands.User;
using MVCApp.Infrastructure.Services;

namespace MVCApp.Infrastructure.CommandHandlers.User
{
    public class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RegisterUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, 
                command.Ign, command.Password, SystemRole.User);
        }
    }
}