using Microsoft.AspNetCore.Identity;

namespace Entities.Users
{
    public class Customer : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Sex { get; set; }

        public virtual PassportData? PassportData { get; set; }
        public virtual List<Bill>? Bills { get; set; }
    }
}
