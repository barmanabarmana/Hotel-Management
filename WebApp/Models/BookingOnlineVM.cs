using Models;
using Models.Users;

namespace WebApp.Models
{
    public class BookingOnlineVM
    {
        public TourModel Tour { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
