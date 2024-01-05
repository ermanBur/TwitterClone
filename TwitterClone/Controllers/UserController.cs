using Microsoft.AspNetCore.Mvc;
using TwitterCloneApplication.Models;
using TwitterCloneApplication.Service; 
using System.Threading.Tasks;

namespace TwitterCloneApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Tüm kullanıcıları listele
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // Kullanıcı detaylarını görüntüle
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Yeni kullanıcı oluşturma formunu görüntüle
        public IActionResult Create()
        {
            return View();
        }

        // Yeni kullanıcı oluşturma işlemini gerçekleştir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid != null)
            {
                try
                {
                    await _userService.CreateUserAsync(user);
                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction("Index", "User");
                }
                catch (Exception ex)
                {

                    TempData["ErrorMessage"] = "An error occurred while creating the user: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "There are some validation errors. Please correct them and try again.";
            }

            return View(user);
        }








        // ... Diğer CRUD işlemleri ...
    }
}
