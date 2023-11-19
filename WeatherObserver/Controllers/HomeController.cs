using Microsoft.AspNetCore.Mvc;

namespace WeatherObserver.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}