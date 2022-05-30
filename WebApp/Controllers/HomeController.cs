using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        public async Task<IActionResult> Index()
        {
            var HotTours = from t in await _tourService.GetHotTourTemplatesAsync()
                           select t;
            var HotToursVM = new HotToursVM()
            {
                Tours = Tools.Mapper
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