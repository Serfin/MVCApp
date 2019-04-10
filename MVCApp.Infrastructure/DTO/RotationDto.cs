using System.Collections.Generic;

namespace MVCApp.Infrastructure.DTO
{
    public class RotationDto
    {
        public string Creator { get; set; }
        public string League { get; set; }
        public string Type { get; set; }
        public IEnumerable<UserDto> Members { get; set; }
        public int Spots { get; set; }
    }
}