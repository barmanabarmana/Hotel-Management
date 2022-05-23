using AutoMapper;
using BLL.Interfaces;
using DTO;
using DTO.Files;
using DTO.Hotels;
using DTO.Transports;
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
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        public TourController()
        {
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

        // GET: TourController
        [HttpGet]
        public ActionResult TourList(string searchString,
            string TourType,
            string TourCountry,
            string TourDeparturePoint,
            int OrderBy,
            int TourMinDuration = 0,
            int TourMaxDuration = int.MaxValue,
            decimal MinTourPrice = 0,
            decimal MaxTourPrice = int.MaxValue)
        {
            var tours = from t in _tourService.GetAllToursTemplates()
                        select t;

            IQueryable<string> filterTypeQuerry = (from tour in tours
                                                   orderby tour.Type
                                                   select tour.Type)
                                                   .AsQueryable();
            IQueryable<string> filterCountryQuerry = (from tour in tours
                                                      orderby tour.Country
                                                      select tour.Country)
                                                   .AsQueryable();
            var intermediateTransportQuerry = (from tour in tours
                                        select tour.TransportIn)
                                        .AsQueryable();
            IQueryable<string> filterDeparturePointQuerry = (from transport in intermediateTransportQuerry
                                                            orderby transport.DeparturePoint
                                                            select transport.DeparturePoint)
                                                            .AsQueryable();


            if (!string.IsNullOrEmpty(searchString))
            {
                tours = _tourService.FindTourTemplates(tours, searchString);
            }

            if (!string.IsNullOrEmpty(TourType))
            {
                tours = _tourService.FindTourTemplatesByType(tours, TourType);
            }

            if (!string.IsNullOrEmpty(TourCountry))
            {
                tours = _tourService.FindTourTemplatesByCountry(tours, TourCountry);
            }
            if (!string.IsNullOrEmpty(TourDeparturePoint))
            {
                tours = _tourService.FindTourTemplatesByCity(tours, TourDeparturePoint);
            }
            var minTourPriceFound = _tourService.FindCheapestTourPrice(tours);
            var maxTourPriceFound = _tourService.FindExpensivestTourPrice(tours);
            if (MinTourPrice > minTourPriceFound || MaxTourPrice < maxTourPriceFound)
            {
                tours = _tourService.FindTourTemplatesByPrice(tours, MinTourPrice, MaxTourPrice);
            }
            var minTourDurationFound = _tourService.FindShortestTourDuration(tours);
            var maxTourDurationFound = _tourService.FindLongestTourDuration(tours);
            if (TourMinDuration > minTourDurationFound || TourMaxDuration < maxTourDurationFound)
            {
                tours = _tourService.FindTourTemplatesByDuration(tours, TourMinDuration, TourMaxDuration);
            }

            tours = _tourService.GetTourTemplatesOrderBy(tours, OrderBy);

            return View(new TourListVM()
            {
                Types = new SelectList(filterTypeQuerry.Distinct().ToList()),
                Arrive = new SelectList(filterCountryQuerry.Distinct().ToList()),
                DeparturePoint = new SelectList(filterDeparturePointQuerry.Distinct().ToList()),
                TourList = TourControllerMapper
                .Map<IEnumerable<TourDTO>, IEnumerable<TourModel>>(tours).ToList(),
                MinTourPrice = minTourPriceFound,
                MaxTourPrice = maxTourPriceFound,
                MinTourDuration = minTourDurationFound,
                MaxTourDuration = maxTourDurationFound,
            });
        }

        // GET: TourController/Details/5
        public ActionResult Details(int id)
        {
            var tour = _tourService.GetTour(id);
            return View(TourControllerMapper.Map<TourDTO, TourModel>(tour));
        }
        // GET: TourController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TourController/Edit/5
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

        // GET: TourController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TourController/Delete/5
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
