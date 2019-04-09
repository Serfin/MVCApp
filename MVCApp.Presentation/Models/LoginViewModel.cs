using System.ComponentModel.DataAnnotations;

namespace MVCApp.Presentation.Models
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}