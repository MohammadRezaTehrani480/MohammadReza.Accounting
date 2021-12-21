using Microsoft.AspNetCore.Identity;

namespace Accounting.WebAPI.Entities
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
