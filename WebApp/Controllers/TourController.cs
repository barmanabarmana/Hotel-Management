using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using WebApp.Models;
using WebApp.Ninject;

namespace WebApp.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }
        public TourController()
        {
            _tourService = UIDependencyResolver<ITourService>.ResolveDependency();
        }

        IMapper TourControllerMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TourDTO, TourModel>();
            cfg.CreateMap<TourModel, TourDTO>();
        }).CreateMapper();

        // GET: TourController
        public ActionResult TourList()
        {
            return View();
        }
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
                .Map<IEnumerable<TourDTO>, IEnumerable<TourModel>>(tours),
            });
        }

        // GET: TourController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
