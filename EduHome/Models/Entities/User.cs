using Microsoft.AspNetCore.Identity;

namespace EduHome.Models.Entities
{
    public class User : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}
