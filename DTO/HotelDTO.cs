
namespace DTO
{
    public class HotelDTO
    {
        //#hotel_ID|Description|Rooms
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rooms { get; set; }
        public decimal Cost { get; set; }
        public string Location { get; set; }
    }
}
