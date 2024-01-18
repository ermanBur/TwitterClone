using Microsoft.AspNetCore.Mvc;
using TwitterClone.Models;
using System.Threading.Tasks;
using TwitterCloneApplication.Models;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using TwitterClone.Entity;
using TwitterClone.Service;

namespace TwitterClone.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger, IPostService postService)
        {
            _userService = userService;
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool userExists = await _userService.ExistsUserAsync(model.Username, model.Email);
                if (userExists)
                {
                    ModelState.AddModelError("", "Username or email already in use. Please choose another one.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                };

                try
                {
                    await _userService.CreateUserAsync(user, model.Password);
                    return RedirectToAction("Login", "Home");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating user");
                    TempData["ErrorMessage"] = "An error occurred while creating the user: " + ex.Message;
                }
            }
            return View(model);
        }


        public IActionResult Login()
        {            
            return View("~/Views/Home/Login.cshtml", new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUserAsync(model.EmailOrUsername, model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
            }
            return View("~/Views/Home/Login.cshtml", model);
        }
        public async Task<IActionResult> Profile(string username)
        {
            var currentUserIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = string.IsNullOrEmpty(currentUserIdString) ? 0 : int.Parse(currentUserIdString);

            var userInformation = await _userService.GetUserInformationByUsernameAsync(username, currentUserId);
            if (userInformation == null)
            {
                return NotFound();
            }

            var followersCount = await _userService.GetUserFollowersCountAsync(userInformation.Id);
            var followingsCount = await _userService.GetUserFollowingsCountAsync(userInformation.Id);
            var isFollowing = currentUserId > 0 && await _userService.IsFollowingAsync(currentUserId, userInformation.Id);

            var viewModel = new PrivacyViewModel
            {
                User = userInformation,
                Posts = userInformation.Posts,
                FollowersCount = followersCount,
                FollowingsCount = followingsCount,
                IsFollowing = isFollowing
            };

            return View("~/Views/Home/Privacy.cshtml", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Follow(int followingId)
        {
            int followerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _userService.FollowUserAsync(followerId, followingId);
            return RedirectToAction("Profile", new { username = User.Identity.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unfollow(int followingId)
        {
            int followerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _userService.UnfollowUserAsync(followerId, followingId);
            return RedirectToAction("Profile", new { username = User.Identity.Name });
        }




        // GET: User/Index


        /*// GET: User/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetUserById(id.Value); // GetUserById metodu varsayılan olarak senkron olduğu için 'await' kullanılmıyor
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "User deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error occurred while deleting the user.";
            }
            return RedirectToAction(nameof(Index));
        }*/
    }
}
