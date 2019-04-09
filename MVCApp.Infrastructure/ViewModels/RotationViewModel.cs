using System.Collections.Generic;

namespace MVCApp.Infrastructure.ViewModels
{
    public class RotationViewModel
    {
        public string Creator { get; set; }
        public string League { get; set; }
        public string Type { get; set; }
        public IEnumerable<UserViewModel> Members { get; set; }
        public int Spots { get; set; }
    }
}