using DTO.User;

namespace DTO
{
    public class BookingDTO
    {
        //#customer_ID|fromDate|toDate|room
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Room { get; set; }
        public HotelDTO Hotel { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
