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
        public async Task AddHotelAsync(HotelDTO NewHotel)
        {
            await UoW.Hotels.AddAsync(Tools.Mapper.Map<Hotel>(NewHotel));
        }
        public async Task DeleteHotelAsync(int Id)
        {
            await UoW.Hotels.DeleteAsync(Id);
        }

        public async Task UpdateHotelAndHotelRoomsAsync(int HotelId, HotelDTO Hotel, List<HotelRoomDTO> Rooms)
        {
            foreach (var HotelRoom in Rooms)
            {
                HotelRoom.HotelId = HotelId;
                await UoW.HotelsRooms.ModifyAsync(HotelRoom.Id,
                    Tools.Mapper.Map<HotelRoom>(HotelRoom));
            }
            await UoW.Hotels.ModifyAsync(HotelId, Tools.Mapper.Map<Hotel>(Hotel));
        }

        public async Task AddHotelRoomAsync(int HotelId, HotelRoomDTO NewHotelRoom)
        {
            NewHotelRoom.HotelId = HotelId;

            await UoW.HotelsRooms.AddAsync(Tools.Mapper.Map<HotelRoom>(NewHotelRoom));
        }
        public async Task<IEnumerable<HotelDTO>> GetAllHotelsAsync()
        {
            return Tools.Mapper
                .Map<List<HotelDTO>>(await UoW.Hotels.GetAllAsync());
        }

        public async Task<HotelDTO> GetHotel(int Id)
        {
            return Tools.Mapper
                .Map<HotelDTO>(await UoW.Hotels
                .GetAsync(Id));
        }
        public async Task InsertImageHotel(int HotelId, string Name, string Path)
        {
            var hotel = Tools.Mapper
                .Map<HotelDTO>(await
                UoW.Hotels.GetAsync(HotelId));

            await UoW.Images.AddAsync(Tools.Mapper.Map<Image>(
                new ImageDTO(Name, Path, hotel.Id)));
        }
    }
}
