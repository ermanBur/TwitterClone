﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [Authorize]
        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.Posts = _postService.GetPostList();
            return View(model);
        }

        public async Task<IActionResult> Privacy()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var postsDto = await _postService.GetPostsByUserIdAsync(int.Parse(userId));

            var viewModel = new PrivacyViewModel
            {
                Posts = postsDto
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
