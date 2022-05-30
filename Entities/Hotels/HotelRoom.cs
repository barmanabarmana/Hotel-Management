using Entities.Hotels.Times;

namespace Entities.Hotels
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int SleepingPlaces { get; set; }
        public decimal Price { get; set; }
        public virtual List<DTOffset> BookedDays { get; set; }
    }
}
