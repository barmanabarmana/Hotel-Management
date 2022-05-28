using AutoMapper;
using BLL.Interfaces;
using BLL.Ninject;
using DTO.Files;
using DTO.Hotels;
using Entities.Files;
using Entities.Hotels;
using UnitsOfWork.Interfaces;

namespace BLL.Services
{
    public class HotelService : IHotelService
    {
        IUnitOfWork UoW;

        public HotelService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        public HotelService()
        {
            UoW = DependencyResolver.ResolveUoW();
        }

        public void AddHotel(HotelDTO NewHotel)
        {
            UoW.Hotels.Add(Tools.Mapper.Map<Hotel>(NewHotel));
        }

        public void DeleteHotel(int Id)
        {
             UoW.Hotels.DeleteAsync(Id);
        }

        public void UpdateHotelAndHotelRooms(int HotelId, HotelDTO Hotel, List<HotelRoomDTO> Rooms)
        {
            foreach (var HotelRoom in Rooms)
            {
                HotelRoom.HotelId = HotelId;
                UoW.HotelsRooms.ModifyAsync(HotelRoom.Id,
                    Tools.Mapper.Map<HotelRoom>(HotelRoom));
            }
            UoW.Hotels.ModifyAsync(HotelId, Tools.Mapper.Map<Hotel>(Hotel));
        }

        public async void AddHotelRoom(int HotelId, HotelRoomDTO NewHotelRoom)
        {
            HotelRoom room = Tools.Mapper.Map<HotelRoom>(
                new HotelRoomDTO(NewHotelRoom.Name,
                NewHotelRoom.Number, 
                NewHotelRoom.SleepingPlaces,
                NewHotelRoom.Price,
                HotelId));

            await UoW.HotelsRooms.Add(room);
        }

        public IEnumerable<HotelDTO> GetAllHotels()
        {
            return Tools.Mapper
                .Map<List<HotelDTO>>(UoW.Hotels.GetAll(h => 
                h.Rooms));
        }

        public async Task<HotelDTO> GetHotel(int Id)
        {
            return Tools.Mapper
                .Map<HotelDTO>(await UoW.Hotels
                .GetAsync(Id));
        }
        public async Task InsertImageHotel(int HotelId, string Name ,string Path)
        {
            var hotel = Tools.Mapper
                .Map<HotelDTO>(await
                UoW.Hotels.GetAsync(HotelId));

            await UoW.Images.Add(Tools.Mapper.Map<Image>(
                new ImageDTO(Name, Path, hotel.Id)));
        }
    }
}
