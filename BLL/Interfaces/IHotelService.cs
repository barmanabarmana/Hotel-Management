using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Hotels;
using Logic.DTOs;
namespace BLL.Interfaces
{
    public interface IHotelService
    {
        void AddHotel(HotelDTO NewHotel);
        void AddHotelRoom(int HotelId, HotelRoomDTO NewHotelRoom);
        IEnumerable<HotelDTO> GetAllHotels();
        HotelDTO GetHotel(int Id);
        void DeleteHotel(int Id);
    }
}
