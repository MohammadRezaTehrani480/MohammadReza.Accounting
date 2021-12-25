using Microsoft.AspNetCore.Identity;

namespace Accounting.WebAPI.Entities
{
    /*When we do not need DTO classes and want to transfer the user object directly from database
      if we do not want a property of user object to be transfered to the client we can use this attribute
      [System.Text.Json.Serialization.JsonIgnore] above that property*/
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
