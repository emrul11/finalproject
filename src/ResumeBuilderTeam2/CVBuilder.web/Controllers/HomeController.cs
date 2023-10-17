using CVBuilder.Infrastructure;
using CVBuilder.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CVBuilder.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;   
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;           
        }

        public async  Task<IActionResult> Index()
        {         
            return View();
        }
		public async Task<IActionResult> Aboutus()
		{
			return View();
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