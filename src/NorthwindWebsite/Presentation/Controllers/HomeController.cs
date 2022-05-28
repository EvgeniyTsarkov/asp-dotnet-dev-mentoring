using Microsoft.AspNetCore.Mvc;

namespace NorthwindWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly Serilog.ILogger _logger;

        public HomeController(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            throw new Exception("Test Exception");
            return View();
        }
    }
}
