using DTO.Hotels;

namespace BLL.Interfaces
{
    public interface IHotelService
    {
        void AddHotel(HotelDTO NewHotel);
        void AddHotelRoom(int HotelId, HotelRoomDTO NewHotelRoom);
        IEnumerable<HotelDTO> GetAllHotels();
        Task<HotelDTO> GetHotel(int Id);
        void DeleteHotel(int Id);
        Task InsertImageHotel(int Id, string Name, string Path);
        void UpdateHotelAndHotelRooms(int HotelId, HotelDTO Hotel, List<HotelRoomDTO> Rooms);
    }
}
