using Models.Files;

namespace Models.Hotels
{
    public class HotelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public List<HotelRoomModel> Rooms { get; set; }
        public string Address { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}
