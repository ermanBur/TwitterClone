using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TwitterClone.Models;
using TwitterClone.Service;
using TwitterClone.Entity;
using TwitterClone.Dto;

namespace TwitterClone.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public SearchController(IUserService userService, IPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            var searchResults = _userService.SearchUsers(searchQuery).ToList();

            var postSearch = (await _postService.SearchPostsByContentAsync(searchQuery)).ToList();

            var model = new SearchModel
            {
                SearchQuery = searchQuery,
                SearchResults = searchResults,
                PostSearch = postSearch
            };
            return View("~/Views/Search/Index.cshtml", model);
        }
    }

}
