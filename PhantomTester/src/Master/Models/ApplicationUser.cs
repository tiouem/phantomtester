using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Master.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Token Token { get; set; }
    }
}
