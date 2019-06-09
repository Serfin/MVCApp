using System;
using System.Threading.Tasks;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.Commands.User;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Infrastructure.CommandHandlers.User
{
    public class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserService _accountService;

        public RegisterUserHandler(IUserService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(RegisterUser command)
        {
            await _accountService.RegisterAsync(Guid.NewGuid(), command.Email, 
                command.Ign, command.Password, SystemRole.User);
        }
    }
}