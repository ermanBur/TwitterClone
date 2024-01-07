using Microsoft.AspNetCore.Mvc;
using TwitterClone.Models; // Bu kısım modellerinizin olduğu namespace ile aynı olmalıdır.

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

        // GET: Home/Login
        public IActionResult Login()
        {
            // Yeni bir LoginViewModel nesnesi ile Login view'ını döndürüyoruz.
            return View(new LoginViewModel());
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Burada kullanıcı doğrulama işlemleri yapılmalıdır.
                // Doğrulama başarılı ise, kullanıcıyı yönlendirme işlemi yapılır.
                // Örnek: return RedirectToAction("Index", "Home");

                // Bu örnekte doğrulama mantığınızı uygulamanız gerekecektir.
                // Şu anda yalnızca modelin geçerli olduğunu kontrol ediyoruz.
            }

            // Model geçerli değilse veya doğrulama başarısız olursa,
            // modeli form ile birlikte geri döndürüyoruz.
            return View(model);
        }

        // GET: Home/Register
        public IActionResult Register()
        {
            // RegisterViewModel'i kullanarak User klasöründeki Register view'ını döndürüyoruz.
            // Burada yeni bir RegisterViewModel nesnesi yaratılıp view'a gönderilebilir.
            // Örnek: return View(new RegisterViewModel());
            return View("~/Views/User/Register.cshtml");
        }
    }
}
