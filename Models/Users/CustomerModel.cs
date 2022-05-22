
using Microsoft.AspNetCore.Identity;
using Models.Hotels;
using Models.Transports;

namespace Models.Users
{
    public class CustomerModel:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<TourModel> Tours { get; set; }
        public virtual List<HotelRoomReservationModel> HotelRoomReservations { get; set; }
        public virtual List<TransportTicketModel> TransportTickets { get; set; }
    }
}
