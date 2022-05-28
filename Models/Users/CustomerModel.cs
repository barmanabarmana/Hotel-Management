using Microsoft.AspNetCore.Identity;

namespace Models.Users
{
    public class CustomerModel:IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Sex { get; set; }
        public PassportDataModel? Passport { get; set; }
        public List<BillModel>? Bills { get; set; }
    }
}
