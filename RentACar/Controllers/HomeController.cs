using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Models;
using System.Diagnostics;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DbContext DbContext;

        public HomeController(ILogger<HomeController> logger, DbContext ApplicationdbContext)
        {
            _logger = logger;
            DbContext = ApplicationdbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cars()
        {
            return View(DbContext.Cars);
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
