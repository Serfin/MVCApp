using System.ComponentModel.DataAnnotations;

namespace MVCApp.Common.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Insert ign")]
        [MinLength(3, ErrorMessage = "Given ign is too short")]
        [StringLength(100, ErrorMessage = "Given ign is too long")]
        public string Ign { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage =  "Error")]
        [Required(ErrorMessage = "Insert email")]
        [StringLength(200, ErrorMessage = "Given email is too long")]
        public string Email { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm email")]
        [Required(ErrorMessage = "Insert email")]
        [Compare("Email", ErrorMessage = "Emails does not match")]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Error")]
        [Required(ErrorMessage = "Insert password")]
        [MinLength(8, ErrorMessage = "Given password is too short")]
        [StringLength(200, ErrorMessage = "Given password is too long")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Insert password")]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }
    }
}