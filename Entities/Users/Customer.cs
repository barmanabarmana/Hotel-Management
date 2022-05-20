using Entities.Transports;
using Entities.Transports;
using Microsoft.AspNetCore.Identity;

namespace Entities.Users
{
    public class Customer : IdentityUser<int>
    {
        //Firstname|Lastname
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<Tour> Tours { get; set; }
        public virtual List<HotelRoomReservation> HotelRoomReservations { get; set; }
        public virtual List<TransportTicket> TransportTickets { get; set; }
    }
}
