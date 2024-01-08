using Microsoft.AspNetCore.Mvc;
using TwitterClone.Models;
using System.Threading.Tasks;
using TwitterCloneApplication.Models;
using Microsoft.Extensions.Logging;
using System;

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
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    // Şifre hash'leme ve diğer işlemler burada yapılabilir.
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
                if (await _userService.ValidateUserAsync(model.EmailOrUsername, model.Password))
                {
                    // Doğrulama başarılı, kullanıcı oturumunu başlat
                    // Oturum başlatma kodunuzu buraya ekleyin, örneğin bir cookie oluşturmak
                    return RedirectToAction("Index", "Home"); // Kullanıcıyı anasayfaya yönlendir
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View(model);
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserAsync(id.Value);
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
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
