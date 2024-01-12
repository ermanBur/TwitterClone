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
using TwitterClone.Entity; // Claims için

namespace TwitterClone.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: User/Register
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // POST: User/Register
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
                    // Şifre ve diğer gerekli alanlar burada oluşturulmalıdır
                };

                try
                {
                    await _userService.CreateUserAsync(user, model.Password);
                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction("Login", "User");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating user");
                    TempData["ErrorMessage"] = "An error occurred while creating the user: " + ex.Message;
                }
            }
            return View(model);
        }


        // GET: User/Login
        public IActionResult Login()
        {            
            return View("~/Views/Home/Login.cshtml", new LoginViewModel());
        }

        // POST: User/Login
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
                        // Diğer gerekli claimler burada eklenebilir
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
            }
            return View("~/Views/Home/Login.cshtml", model);
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
