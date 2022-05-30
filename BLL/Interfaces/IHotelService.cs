using DTO.Hotels;

namespace BLL.Interfaces
{
    public interface IHotelService
    {
        Task AddHotelAsync(HotelDTO NewHotel);
        Task AddHotelRoomAsync(int HotelId, HotelRoomDTO NewHotelRoom);
        Task<IEnumerable<HotelDTO>> GetAllHotelsAsync();
        Task<HotelDTO> GetHotel(int Id);
        Task DeleteHotelAsync(int Id);
        Task InsertImageHotel(int Id, string Name, string Path);
        Task UpdateHotelAndHotelRoomsAsync(int HotelId, HotelDTO Hotel, List<HotelRoomDTO> Rooms);
    }
}
