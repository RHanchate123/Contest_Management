using Microsoft.AspNetCore.Identity;


namespace IdentityApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            TwoFactorEnabled = true;
            PhoneNumberConfirmed = true;
            EmailConfirmed = true;

        }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string NormalizedEmail { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
