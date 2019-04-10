using System;
using System.Threading.Tasks;
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
        // TODO : Add role 
        public async Task HandleAsync(RegisterUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, 
                command.Ign, command.Password, "User");
        }
    }
}