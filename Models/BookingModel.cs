using Models.Users;

namespace Models
{
    public class BookingModel
    {
        //#customer_ID|fromDate|toDate|room
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Room { get; set; }
        public HotelModel Hotel { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
