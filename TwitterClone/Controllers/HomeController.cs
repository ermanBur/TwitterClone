using Microsoft.AspNetCore.Mvc;
using TwitterClone.Models; 

namespace TwitterClone.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return View(model);
        }

        // GET: Home/Register
        public IActionResult Register()
        {
            return View("~/Views/User/Register.cshtml");
        }
    }
}
