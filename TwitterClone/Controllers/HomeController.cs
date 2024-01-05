using Microsoft.AspNetCore.Mvc;

namespace TwitterCloneApplication.Controllers
{
    public class HomeController : Controller
    {
        // Anasayfayı göster
        public IActionResult Index()
        {
            return View();
        }

        // Gizlilik politikasını göster
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
