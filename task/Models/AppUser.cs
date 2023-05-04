using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}
