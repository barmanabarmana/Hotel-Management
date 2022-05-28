﻿using AutoMapper;
using DTO;
using DTO.Files;
using DTO.Hotels;
using DTO.Hotels.Times;
using DTO.Transports;
using DTO.User;
using Entities;
using Entities.Users;
using Models;
using Models.Files;
using Models.Hotels;
using Models.Hotels.Times;
using Models.Transports;
using Models.Users;
using System.Globalization;
using WebApp.Models;

namespace WebApp
{
    public static class Tools
    {
        public static IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Bill, BillDTO>();
            cfg.CreateMap<BillDTO, BillModel>();
            cfg.CreateMap<PassportDataDTO, PassportDataModel>();
            cfg.CreateMap<PassportDataModel, PassportDataDTO>();
            cfg.CreateMap<Customer, CustomerModel>();
            cfg.CreateMap<CustomerDTO, CustomerModel>();
            cfg.CreateMap<CustomerModel, CustomerDTO>();
            cfg.CreateMap<TourDTO, TourModel>();
            cfg.CreateMap<TourModel, TourDTO>();
            cfg.CreateMap<HotelDTO, HotelModel>();
            cfg.CreateMap<HotelModel, HotelDTO>();
            cfg.CreateMap<HotelRoomDTO, HotelRoomModel>();
            cfg.CreateMap<HotelRoomModel, HotelRoomDTO>();
            cfg.CreateMap<HotelRoomReservationModel, HotelRoomReservationDTO>();
            cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservationModel>();
            cfg.CreateMap<TransportDTO, TransportModel>();
            cfg.CreateMap<TransportPlaceDTO, TransportPlaceModel>();
            cfg.CreateMap<TransportModel, TransportDTO>();
            cfg.CreateMap<TransportPlaceModel, TransportPlaceDTO>();
            cfg.CreateMap<ImageDTO, ImageModel>();
            cfg.CreateMap<ImageModel, ImageDTO>();
            cfg.CreateMap<DTOffsetModel, DTOffsetDTO>();
            cfg.CreateMap<DTOffsetDTO, DTOffsetModel>();
        }).CreateMapper();

        public static decimal ToDecimal(this string str)
        {
            if (str == null)
            {
                return 0;
            }
            str = str.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
            return decimal.Parse(str, CultureInfo.InvariantCulture);
        }
        public static decimal ToDecimal(this decimal price)
        {
            var str = price.ToString();
            return str.ToDecimal();
        }
    }

}
