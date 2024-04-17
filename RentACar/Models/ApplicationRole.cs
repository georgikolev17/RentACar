using Microsoft.AspNetCore.Identity;

namespace RentACar.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) 
            : base(roleName)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
