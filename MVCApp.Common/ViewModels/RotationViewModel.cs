using System;
using System.Collections.Generic;

namespace MVCApp.Common.ViewModels
{
    public class RotationViewModel
    {
        public string Creator { get; set; }
        public string League { get; set; }
        public string Type { get; set; }
        public IEnumerable<UserViewModel> Members { get; set; }
        public int Spots { get; set; }
        public DateTime CreationTime { get; set; }
    }
}