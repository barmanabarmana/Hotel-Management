using AutoMapper;
using BLL.Interfaces;
using BLL.Ninject;
using DTO.Hotels;
using Entities.Hotels;
using UnitsOfWork.Interfaces;

namespace BLL.Services
{
    public class HotelService : IHotelService
    {
        IUnitOfWork UoW;

        public HotelService(IUnitOfWork UoW)
        {
            HotelLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, Hotel>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();

            this.UoW = UoW;
        }

        IMapper HotelLogicMapper;

        public HotelService()
        {
            HotelLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, Hotel>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();

            UoW = DependencyResolver.ResolveUoW();
        }

        public void AddHotel(HotelDTO NewHotel)
        {
            UoW.Hotels.Add(HotelLogicMapper.Map<HotelDTO, Hotel>(NewHotel));
        }

        public void DeleteHotel(int Id)
        {
            UoW.Hotels.Delete(Id);
        }

        public void AddHotelRoom(int HotelId, HotelRoomDTO NewHotelRoom)
        {
            Hotel hotel = UoW.Hotels.GetAll(x => x.Id == HotelId, x => x.Rooms).FirstOrDefault();

            HotelRoom room = HotelLogicMapper.Map<HotelRoomDTO, HotelRoom>(NewHotelRoom);

            room.Hotel = hotel;

            hotel.Rooms.Add(room);

            UoW.Hotels.Modify(hotel.Id, hotel);
        }

        public IEnumerable<HotelDTO> GetAllHotels()
        {
            return HotelLogicMapper
                .Map<IEnumerable<Hotel>, List<HotelDTO>>(UoW.
                Hotels.GetAll(h => 
                h.Rooms));
        }

        public HotelDTO GetHotel(int Id)
        {
            return HotelLogicMapper
                .Map<Hotel, HotelDTO>(UoW.Hotels
                .GetAll(x =>
                x.Id == Id, x => 
                x.Rooms)
                .FirstOrDefault());
        }
    }
}
