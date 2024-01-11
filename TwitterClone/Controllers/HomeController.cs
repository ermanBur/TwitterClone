using Microsoft.AspNetCore.Mvc;
using TwitterClone.Models;
using TwitterClone.Service;

namespace TwitterClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.Posts = _postService.GetPostList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            var model = new PrivacyViewModel();
            model.Posts = _postService.GetPostList();
            return View(model);
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
            return RedirectToAction("Register", "User");
        }
            
        public IActionResult EditProfile()
        {
            return View();  
        }public IActionResult Settings()
        {
            return View();
        }

    }
}
