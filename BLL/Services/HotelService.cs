﻿using AutoMapper;
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

        IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<HotelDTO, Hotel>();
            cfg.CreateMap<HotelRoomDTO, HotelRoom>();
            cfg.CreateMap<Hotel, HotelDTO>();
            cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            cfg.CreateMap<ImageDTO, Image>();
            cfg.CreateMap<Image, ImageDTO>();
        }).CreateMapper();

        public void AddHotel(HotelDTO NewHotel)
        {
            UoW.Hotels.Add(Mapper.Map<HotelDTO, Hotel>(NewHotel));
        }

        public void DeleteHotel(int Id)
        {
            UoW.Hotels.Delete(Id);
        }

        public void AddHotelRoom(int HotelId, HotelRoomDTO NewHotelRoom)
        {
            Hotel hotel = UoW.Hotels.GetAll(x => x.Id == HotelId, x => x.Rooms).FirstOrDefault();

            HotelRoom room = Mapper.Map<HotelRoomDTO, HotelRoom>(NewHotelRoom);

            room.Hotel = hotel;

            hotel.Rooms.Add(room);

            UoW.Hotels.Modify(hotel.Id, hotel);
        }

        public IEnumerable<HotelDTO> GetAllHotels()
        {
            return Mapper
                .Map<IEnumerable<Hotel>, List<HotelDTO>>(UoW.
                Hotels.GetAll(h => 
                h.Rooms));
        }

        public HotelDTO GetHotel(int Id)
        {
            return Mapper
                .Map<Hotel, HotelDTO>(UoW.Hotels
                .GetAll(x =>
                x.Id == Id, x => 
                x.Rooms)
                .FirstOrDefault());
        }
        public void InsertImageHotel(int Id, string Path)
        {
            var hotel = Mapper
                .Map<Hotel, HotelDTO>(
                UoW.Hotels.Get(Id));

            hotel.Images.Add(
                new ImageDTO(Path));

            UoW.Hotels.Modify(Id, Mapper
                .Map<HotelDTO, Hotel>(hotel));
        }
    }
}
