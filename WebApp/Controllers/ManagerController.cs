using AutoMapper;
using BLL.Interfaces;
using DTO;
using DTO.Files;
using DTO.Hotels;
using DTO.Transports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Files;
using Models.Hotels;
using Models.Transports;
using WebApp.Models;
using WebApp.Ninject;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class ManagerController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IHotelService _hotelService;
        private readonly ITransportService _transportService;
        public ManagerController()
        {
            _tourService = UIDependencyResolver<ITourService>.ResolveDependency();
            _hotelService = UIDependencyResolver<IHotelService>.ResolveDependency();
            _transportService = UIDependencyResolver<ITransportService>.ResolveDependency();
        }
        IMapper ControllerMapper = new MapperConfiguration(cfg =>
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

        // GET: ManagerController
        public ActionResult Manage()
        {
            return View(ControllerMapper
                .Map<IEnumerable<TourDTO>, IEnumerable<TourModel>>(_tourService.
                GetAllToursTemplates()));
        }

        // GET: ManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManagerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
