﻿using BLL.Interfaces;
using DTO;
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
        public TourController()
        {
            _tourService = UIDependencyResolver<ITourService>.ResolveDependency();
        }

        // GET: TourController
        [HttpGet]
        public async Task<IActionResult> TourList(string searchString,
            string TourType,
            string TourCountry,
            string TourDeparturePoint,
            int OrderBy,
            string MinTourPrice,
            string MaxTourPrice,
            int TourMinDuration = 0,
            int TourMaxDuration = int.MaxValue)
        {
            var tours = from t in await _tourService.GetAllToursTemplates()
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

            decimal inputMinPriceFilter = 0;

            if (MinTourPrice != null)
            {
                inputMinPriceFilter = MinTourPrice.ToDecimal();
            }

            decimal inputMaxPriceFilter = decimal.MaxValue;

            if (MaxTourPrice != null)
            {
                inputMaxPriceFilter = MaxTourPrice.ToDecimal();
            }

            int minTourPriceFound = (int)_tourService.FindCheapestTourPrice(tours);
            int maxTourPriceFound = (int)_tourService.FindExpensivestTourPrice(tours);
            if (inputMinPriceFilter > minTourPriceFound || inputMaxPriceFilter < maxTourPriceFound)
            {
                tours = _tourService.FindTourTemplatesByPrice(tours, inputMinPriceFilter, inputMaxPriceFilter);
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
                TourList = Tools.Mapper
                .Map<IEnumerable<TourDTO>, IEnumerable<TourModel>>(tours).ToList(),
                MinTourPrice = minTourPriceFound.ToString(),
                MaxTourPrice = maxTourPriceFound.ToString(),
                MinTourDuration = minTourDurationFound,
                MaxTourDuration = maxTourDurationFound,
            });
        }

        // GET: TourController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tour = await _tourService.GetTourAsync(id);
            return View(Tools.Mapper.Map<TourDTO, TourModel>(tour));
        }
    }
}
