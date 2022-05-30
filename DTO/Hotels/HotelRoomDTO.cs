using DTO.Hotels.Times;

namespace DTO.Hotels
{
    public class HotelRoomDTO
    {
        public HotelRoomDTO() { }
        public HotelRoomDTO(string Name, int Number, int SleepingPlaces, decimal Price, int HotelId)
        {
            this.Name = Name;
            this.Number = Number;
            this.SleepingPlaces = SleepingPlaces;
            this.Price = Price;
            this.HotelId = HotelId;
            BookedDays = new List<DTOffsetDTO>();
        }

        public int Id { get; set; }
        public int HotelId { get; set; }
        public HotelDTO Hotel { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int SleepingPlaces { get; set; }
        public decimal Price { get; set; }
        public List<DTOffsetDTO> BookedDays { get; set; }
    }
}
