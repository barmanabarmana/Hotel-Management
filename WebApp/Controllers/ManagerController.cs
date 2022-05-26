using AutoMapper;
using BLL.Interfaces;
using DTO;
using DTO.Files;
using DTO.Hotels;
using DTO.Transports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Files;
using Models.Hotels;
using Models.Transports;
using System.Globalization;
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
        private IWebHostEnvironment _appEnvironment;
        public ManagerController(IWebHostEnvironment appEnvironment)
        {
            _tourService = UIDependencyResolver<ITourService>.ResolveDependency();
            _hotelService = UIDependencyResolver<IHotelService>.ResolveDependency();
            _transportService = UIDependencyResolver<ITransportService>.ResolveDependency();
            _appEnvironment = appEnvironment;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ManageTours(string SearchString)
        {
            var tours = _tourService.
                GetAllToursTemplates();
            if (!string.IsNullOrEmpty(SearchString))
            {
                tours = _tourService.FindTourTemplates(tours, SearchString);
            }
            var manageToursVM = new ManageToursVM()
            {
                Tours = Tools.Mapper
                .Map<IEnumerable<TourDTO>, IEnumerable<TourModel>>(tours).ToList(),
            };
            return View(manageToursVM);
        }

        public ActionResult CreateTour()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTour(TourVM tourModel)
        {
            var tour = tourModel.Tour;

            tour.Price = tourModel.TourPrice.ParsingString();

            tour.Hotel = tourModel.Hotel;

            tour = Tools.Mapper
                .Map<TourDTO, TourModel>(_transportService
                .AddTransportToTour(Tools.Mapper.Map<TourModel, TourDTO>(tourModel.Tour),
                Tools.Mapper.Map<TransportModel, TransportDTO>(tourModel.TransportIn),
                tourModel.AvailibleSeatsOut,
                tourModel.PriceForTicketIn.ParsingString(), 
                Tools.Mapper.Map<TransportModel, TransportDTO>(tourModel.TransportOut),
                tourModel.AvailibleSeatsOut,
                tourModel.PriceForTicketOut.ParsingString()));

            _tourService.AddTour(Tools.Mapper
                .Map<TourModel, TourDTO>(tour));

            return RedirectToAction(nameof(ManageTours));
        }

        public ActionResult EditTour(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var tour = Tools.Mapper.Map<TourDTO, TourModel>(_tourService.GetTour(id));
            if (tour == null)
            {
                return NotFound();
            }
            var tourModel = new TourVM()
            {
                Tour = tour,
                TourPrice = tour.Price.ToString(),
                Hotel = tour.Hotel,
                Rooms = tour.Hotel.Rooms,
                TransportIn = tour.Transports.FirstOrDefault(),
                PriceForTicketIn = tour.Transports.FirstOrDefault().TransportPlaces[0].Price.ToString(),
                TransportOut = tour.Transports.LastOrDefault(),
                PriceForTicketOut = tour.Transports.FirstOrDefault().TransportPlaces[0].Price.ToString(),
            };
            return View(tourModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTour(int id, TourVM tourModel)
        {
            UploadFile(tourModel.Hotel.Id, tourModel.UploadedFile);
            if (tourModel.TourPrice != null)
            {
                tourModel.Tour.Price = tourModel.TourPrice.ParsingString();
            }

            tourModel.Tour.Hotel = tourModel.Hotel;

            _transportService.ApplyNewPriceForTicketAndUpdateTransport(Tools.Mapper
                .Map<TransportModel, TransportDTO>(tourModel.TransportIn),
                tourModel.PriceForTicketIn.ParsingString());

            _transportService.ApplyNewPriceForTicketAndUpdateTransport(Tools.Mapper
                .Map<TransportModel, TransportDTO>(tourModel.TransportOut),
                tourModel.PriceForTicketOut.ParsingString());

            if (tourModel.Rooms != null && tourModel.Rooms.Count > 0)
            {
                _hotelService.UpdateHotelAndHotelRooms(tourModel.Hotel.Id, Tools.Mapper.
                    Map<HotelModel, HotelDTO>(tourModel.Hotel), Tools.Mapper.
                    Map<List<HotelRoomModel>, List<HotelRoomDTO>>(tourModel.Rooms));
            }
            if (tourModel.NewRoom.Number != 0)
            {
                _hotelService.AddHotelRoom(tourModel.Hotel.Id,
                    Tools.Mapper.Map<HotelRoomModel, HotelRoomDTO>(tourModel.NewRoom));
            }

            _tourService.UpdateTour(id,
                Tools.Mapper.Map<TourModel, TourDTO>(tourModel.Tour));
            return RedirectToAction(nameof(EditTour));
        }
        public void UploadFile(int HotelId, IFormFile uploadedFile)
        {

            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }
                _hotelService.InsertImageHotel(HotelId, uploadedFile.FileName, path);
            }
        }

        public ActionResult DeleteTour(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var tourModel = _tourService.GetTour(id);
            if (tourModel == null)
            {
                return NotFound();
            }

            return View(Tools.Mapper.Map<TourDTO, TourModel>(tourModel));
        }

        [HttpPost, ActionName("DeleteTour")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTourConfirmed(int id)
        {
            if (_tourService.GetTour(id) == null)
            {
                return NotFound();
            }

            _tourService.DeleteTour(id);

            return RedirectToAction(nameof(ManageTours));
        }
    }
}
