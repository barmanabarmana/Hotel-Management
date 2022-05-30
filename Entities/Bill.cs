using Entities.Hotels;
using Entities.Transports;
using Entities.Users;

namespace Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime Created { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual HotelRoomReservation RoomReservation { get; set; }
        public virtual List<TransportTicket> Tickets { get; set; }
        public int CustomerWhoBookId { get; set; }
        public virtual Customer CustomerWhoBook { get; set; }
        public virtual List<Customer>? Tourists { get; set; }
    }
}
