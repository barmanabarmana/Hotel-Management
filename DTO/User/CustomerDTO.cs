using Microsoft.AspNetCore.Identity;

namespace DTO.User
{
    public class CustomerDTO : IdentityUser<int>
    {
        public CustomerDTO() { }
        public CustomerDTO(string Firstname, string Lastname, string Login, string Password)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            UserName = Login;
            PasswordHash = Password;
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Sex { get; set; }

        public PassportDataDTO? PassportData { get; set; }
        public List<BillDTO>? Bills { get; set; }
    }
}
