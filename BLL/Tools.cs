using AutoMapper;
using Entities.Users;
using DTO.User;
using Entities.Transports;
using DTO.Hotels;
using Entities;
using DTO;
using DTO.Transports;
using Entities.Hotels;
using DTO.Files;
using Entities.Files;
using Entities.Hotels.Times;
using DTO.Hotels.Times;

namespace BLL
{
    internal static class Tools
    {
        public static IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Bill, BillDTO>();
            cfg.CreateMap<BillDTO, Bill>();
            cfg.CreateMap<PassportData, PassportDataDTO>();
            cfg.CreateMap<PassportDataDTO, PassportData>();
            cfg.CreateMap<CustomerDTO, Customer>();
            cfg.CreateMap<Customer, CustomerDTO>();
            cfg.CreateMap<ImageDTO, Image>();
            cfg.CreateMap<Image, ImageDTO>();
            cfg.CreateMap<Transport, TransportDTO>();
            cfg.CreateMap<TransportDTO, Transport>();
            cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
            cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
            cfg.CreateMap<TourDTO, Tour>();
            cfg.CreateMap<Tour, TourDTO>();
            cfg.CreateMap<Hotel, HotelDTO>();
            cfg.CreateMap<HotelDTO, Hotel>();
            cfg.CreateMap<HotelRoomDTO, HotelRoom>();
            cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservation>();
            cfg.CreateMap<HotelRoomReservation, HotelRoomReservationDTO>();
            cfg.CreateMap<DTOffset, DTOffsetDTO>();
            cfg.CreateMap<DTOffsetDTO, DTOffset>();
        }).CreateMapper();
    }
}
