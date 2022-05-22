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
        public ActionResult TourList(string searchString = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string type = null,
            string country = null,
            string city = null)
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
            IQueryable<string> filterCityQuerry = (from tour in tours
                                                   orderby tour.City
                                                   select tour.City)
                                                   .AsQueryable();


            if (!string.IsNullOrEmpty(searchString))
            {
                tours = _tourService.FindTourTemplates(tours, searchString);
            }

            if (!string.IsNullOrEmpty(type))
            {
                tours = _tourService.FindTourTemplatesByType(tours, type);
            }

            if (!string.IsNullOrEmpty(country))
            {
                tours = _tourService.FindTourTemplatesByCountry(tours, country);
            }
            if (!string.IsNullOrEmpty(city))
            {
                tours = _tourService.FindTourTemplatesByCity(tours, city);
            }
            if(MinPrice > 0 || MaxPrice < decimal.MaxValue)
            {
                tours = _tourService.FindTourTemplatesByPrice(tours, MinPrice, MaxPrice);
            }
            return View(new TourListVM()
            {
                Type = new SelectList(filterTypeQuerry.Distinct().ToList()),
                Country = new SelectList(filterTypeQuerry.Distinct().ToList()),
                City = new SelectList(filterTypeQuerry.Distinct().ToList()),
                TourList = TourControllerMapper
                .Map<IEnumerable<TourDTO>, IEnumerable<TourModel>>(tours).ToList(),
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
