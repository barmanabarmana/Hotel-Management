using Models.Hotels;
using Models.Transports;
using Models.Users;

namespace Models
{
    public class BillModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime Created { get; set; }
        public int TourId { get; set; }
        public TourModel Tour { get; set; }
        public HotelRoomReservationModel RoomReservation { get; set; }
        public List<TransportTicketModel> Tickets { get; set; }
        public int CustomerWhoBookId { get; set; }
        public CustomerModel CustomerWhoBook { get; set; }
        public List<CustomerModel>? Tourists { get; set; }
    }
}
