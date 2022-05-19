using Microsoft.AspNetCore.Identity;

namespace DTO.User
{
    public class CustomerDTO : IdentityUser<int>
    {
        //Firstname|Lastname
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
