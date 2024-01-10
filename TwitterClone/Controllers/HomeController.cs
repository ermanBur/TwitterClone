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
