using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;
using System.Diagnostics;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarServices carServices;

        public HomeController(ILogger<HomeController> logger, CarServices carServices)
        {
            _logger = logger;
            this.carServices=carServices;
        }

        public IActionResult Index()
        {
            return View();
        }
/*
        [Authorize]
        [Route("/cars")]
        public IActionResult Cars()
        {
            return View(this.carServices.GetAllCars());
        }*/

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
