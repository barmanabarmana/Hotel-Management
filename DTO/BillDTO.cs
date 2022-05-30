using DTO.Hotels;
using DTO.Transports;
using DTO.User;

namespace DTO
{
    public class BillDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime Created { get; set; }
        public int TourId { get; set; }
        public TourDTO Tour { get; set; }
        public HotelRoomReservationDTO RoomReservation { get; set; }
        public List<TransportTicketDTO> Tickets { get; set; }
        public int CustomerWhoBookId { get; set; }
        public CustomerDTO CustomerWhoBook { get; set; }
        public List<CustomerDTO>? Tourists { get; set; }
    }
}
