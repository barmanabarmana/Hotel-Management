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
using Models;
using Models.Files;
using Models.Hotels;
using Models.Transports;
using Models.Users;
using WebApp.Models;
using WebApp.Ninject;

namespace WebApp.Controllers
{
    public class BookingCotroller:Controller
    {
        private readonly ITourService _tourService;
        private readonly IUserService _userService;
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;
        public BookingCotroller(SignInManager<Customer> signInManager, UserManager<Customer> userManager)
        {
            _userService = UIDependencyResolver<IUserService>.ResolveDependency();
            _tourService = UIDependencyResolver<ITourService>.ResolveDependency();
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult BookingOnline(int id)
        {
            var tour = _tourService.GetTour(id);
            var bookingVM = new BookingOnlineVM()
            {
                Tour = Tools.Mapper.Map<TourDTO, TourModel>(tour),
                Customer = Tools.Mapper
                .Map<CustomerDTO, CustomerModel>(_userService.
                GetUser(int.Parse(_userManager
                .GetUserId(User)))),
            };
            return View(tour);
        }
    }
}
