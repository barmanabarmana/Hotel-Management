using Models.Hotels.Times;

namespace Models.Hotels
{
    public class HotelRoomModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public HotelModel Hotel { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int SleepingPlaces { get; set; }
        public int Price { get; set; }
        public List<DTOffsetModel> BookedDays { get; set; }
    }
}
