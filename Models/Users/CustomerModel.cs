
using Microsoft.AspNetCore.Identity;

namespace Models.Users
{
    public class CustomerModel:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
