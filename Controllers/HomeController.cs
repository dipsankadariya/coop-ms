using System.Diagnostics;
using bms.Models;
using bms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService; 

        public HomeController(ILogger<HomeController> logger,IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardData = await _homeService.GetDashboardStatisticsAsync();
            return View(dashboardData);
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
