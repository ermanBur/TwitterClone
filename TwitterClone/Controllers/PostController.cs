using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwitterClone.Dto;
using TwitterClone.Entity;
using TwitterClone.Service;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneApplication.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TwitterClone.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PostController(IPostService postService, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(int id)
        {
            if (id != null)
            {
                var post = _postService.GetPost(id);
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult<PostDto>> DeletePostById(int id)
        {
            await _postService.DeletePostById(id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto createPostDto)
        {
            var userIdString = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("User ID is invalid.");
            }

            var result = await _postService.CreatePost(createPostDto, userId);
            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound();
            }


        }

        [HttpPost("retweet/{postId}")]
        public async Task<IActionResult> Retweet(int postId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("User ID is invalid.");
            }

            var response = await _postService.ToggleRetweetAsync(postId, userId);

            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Privacy", "Home");
        }










    }
}
