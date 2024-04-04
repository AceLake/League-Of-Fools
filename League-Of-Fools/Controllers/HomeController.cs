using League_Of_Fools.Models;
using League_Of_Fools.Service;
using League_Of_Fools.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace League_Of_Fools.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MyLogger.GetInstance().Info(this.GetType().Name, "In Index");
            return View();
        }

        public IActionResult Privacy()
        {
            MyLogger.GetInstance().Info(this.GetType().Name, "In Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}