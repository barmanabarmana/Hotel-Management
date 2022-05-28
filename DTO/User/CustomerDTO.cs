using Microsoft.AspNetCore.Identity;

namespace DTO.User
{
    public class CustomerDTO : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Sex { get; set; }
        
        public PassportDataDTO? PassportData { get; set; }
        public List<BillDTO>? Bills { get; set; }
    }
}
