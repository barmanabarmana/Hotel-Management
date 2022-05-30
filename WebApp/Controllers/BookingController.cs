using AutoMapper;
using BLL.Interfaces;
using DTO;
using DTO.Files;
using DTO.Hotels;
using DTO.Transports;
using DTO.User;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Files;
using Models.Hotels;
using Models.Transports;
using Models.Users;
using WebApp.Models;
using WebApp.Ninject;

namespace WebApp.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IUserService _userService;
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;
        public BookingController(SignInManager<Customer> signInManager, UserManager<Customer> userManager)
        {
            _userService = UIDependencyResolver<IUserService>.ResolveDependency();
            _tourService = UIDependencyResolver<ITourService>.ResolveDependency();
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> BookingOnline(int id)
        {
            var tour = await _tourService.GetTourAsync(id);

            var hotelRoomNames = (from room in tour.Hotel.Rooms
                                  where room.SleepingPlaces >= tour.AviablePeopleCount
                                  orderby room.Price
                                  select room.Name).AsQueryable();

            List<CustomerModel> additionalTourists = new ();

            if(tour.AviablePeopleCount > 1) 
            { 
                for (int i = 1; i < tour.AviablePeopleCount; i++)
                {
                    additionalTourists.Add(new CustomerModel());
                }
            }

            var bookingVM = new BookingOnlineVM()
            {
                Tour = Tools.Mapper.Map<TourDTO, TourModel>(tour),
                CustomerWhoBook = Tools.Mapper
                .Map<CustomerModel>(await _userService
                .GetUserAsync(int.Parse(_userManager.GetUserId(User)))),
                HotelRoomNames = new SelectList(hotelRoomNames.Distinct().ToList()),
                AdditionalTourist = additionalTourists,
                DepositAmount = (tour.Price / 4).ToString(),
            };
            return View(bookingVM);
        }
        [HttpPost]
        public async Task<IActionResult> BookingOnline(BookingOnlineVM booking)
        {
            var bill = Tools.Mapper.Map<BillModel>(await _userService
                .BuildBillAsync(booking.CustomerWhoBook.Id,
                Tools.Mapper.Map<List<CustomerDTO>>(booking.AdditionalTourist),
                booking.DepositAmount.ToDecimal(),
                booking.Tour.Id,
                booking.RoomName,
                booking.Tour.TransportIn.ArrivalTime,
                booking.Tour.TransportOut.DepartureTime));

            return View(Order(bill));
        }
        public IActionResult Order(BillModel bill)
        {
            return View(bill);
        }

        public async Task<IActionResult> ShowBills()
        {
            var bills = Tools
                .Mapper.Map<List<BillModel>>(await _userService
                .GetBills((await _userManager.GetUserAsync(User)).Id));

            return View(bills);
        }
    }
}
