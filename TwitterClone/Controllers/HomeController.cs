using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwitterClone.Models;
using TwitterClone.Service;

namespace TwitterClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;


        public HomeController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;

        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var feedPosts = await _postService.GetFeedForUserAsync(currentUserId);
            var model = new IndexViewModel
            {
                Posts = feedPosts
            };
            return View(model);
        }

        public async Task<IActionResult> Privacy(string username)
        {
            var currentUserIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = string.IsNullOrEmpty(currentUserIdString) ? 0 : int.Parse(currentUserIdString);

            if (string.IsNullOrEmpty(username))
            {
                username = User.Identity.Name; 
            }

            var userInformation = await _userService.GetUserInformationByUsernameAsync(username, currentUserId);
            if (userInformation == null)
            {
                return NotFound();
            }

            var followersCount = await _userService.GetUserFollowersCountAsync(userInformation.Id);
            var followingsCount = await _userService.GetUserFollowingsCountAsync(userInformation.Id);
            var posts = await _postService.GetPostsByUserIdAsync(userInformation.Id);

            var viewModel = new PrivacyViewModel
            {
                User = userInformation,
                Posts = posts,
                FollowersCount = followersCount,
                FollowingsCount = followingsCount
                // IsFollowing bilgisini de ekleyebilirsiniz, eğer modelde varsa ve gerekliyse.
            };

            return View(viewModel);
        }






        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

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
