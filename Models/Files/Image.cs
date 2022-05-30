using Models.Hotels;

namespace Models.Files
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int HotelId { get; set; }
        public HotelModel Hotel { get; set; }
    }
}
