using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace RentACar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.EmailConfirmed = true;
        }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? EGN { get; set; }
    }
}
