using AutoMapper;
using BLL.Interfaces;
using DTO;
using DTO.Files;
using DTO.Hotels;
using DTO.Transports;
using DTO.User;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Files;
using Models.Hotels;
using Models.Transports;
using Models.Users;
using WebApp.Ninject;

namespace WebApp.Controllers
{
    public class BookingCotroller:Controller
    {
        private readonly ITourService _tourService;
        private readonly IUserService _userService;
        public BookingCotroller()
        {
            _userService = UIDependencyResolver<IUserService>.ResolveDependency();
            _tourService = UIDependencyResolver<ITourService>.ResolveDependency();
        }

        IMapper TourControllerMapper = new MapperConfiguration(cfg =>
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

        public IActionResult BookingOnline(int id)
        {
            var tour = _tourService.GetTour(id);
            return View();
        }
    }
}
