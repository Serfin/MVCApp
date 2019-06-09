namespace MVCApp.Infrastructure.Commands.User
{
    public class RegisterUser : ICommand
    {
        public string Ign { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}