using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Users
{
    public class Customer : IdentityUser<int>
    {
        //Firstname|Lastname
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
