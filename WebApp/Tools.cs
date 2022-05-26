using AutoMapper;
using DTO;
using DTO.Files;
using DTO.Hotels;
using DTO.Transports;
using DTO.User;
using Models;
using Models.Files;
using Models.Hotels;
using Models.Transports;
using Models.Users;
using System.Globalization;

namespace WebApp
{
    public static class Tools
    {
        public static IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CustomerDTO, CustomerModel>();
            cfg.CreateMap<CustomerModel, CustomerDTO>();
            cfg.CreateMap<TourDTO, TourModel>();
            cfg.CreateMap<TourModel, TourDTO>();
            cfg.CreateMap<HotelDTO, HotelModel>();
            cfg.CreateMap<HotelModel, HotelDTO>();
            cfg.CreateMap<HotelRoomDTO, HotelRoomModel>();
            cfg.CreateMap<HotelRoomModel, HotelRoomDTO>();
            cfg.CreateMap<TransportDTO, TransportModel>();
            cfg.CreateMap<TransportPlaceDTO, TransportPlaceModel>();
            cfg.CreateMap<TransportModel, TransportDTO>();
            cfg.CreateMap<TransportPlaceModel, TransportPlaceDTO>();
            cfg.CreateMap<ImageDTO, ImageModel>();
            cfg.CreateMap<ImageModel, ImageDTO>();
        }).CreateMapper();

        public static decimal ParsingString(this string str)
        {
            if(str == null)
            {
                return 0;
            }
            str = str.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
            return decimal.Parse(str, CultureInfo.InvariantCulture);
        }
    }
}
