using Entities.Files;

namespace Entities.Hotels
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public virtual List<HotelRoom> Rooms { get; set; }
        public string Address { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}
