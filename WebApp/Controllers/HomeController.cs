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
using System.Diagnostics;
using WebApp.Models;
using WebApp.Ninject;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ITourService _tourService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _tourService = UIDependencyResolver<ITourService>.ResolveDependency();
        }

        IMapper TourControllerMapper = new MapperConfiguration(cfg =>
        {
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

        public IActionResult Index()
        {
            var HotTours = from t in _tourService.GetAllHotTourTemplates()
                           select t;
            var HotToursVM = new HotToursVM()
            {
                Tours = TourControllerMapper
                .Map<IEnumerable<TourDTO>, IEnumerable<TourModel>>(HotTours).ToList(),
            };
            return View(HotToursVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}