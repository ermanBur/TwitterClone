using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwitterClone.Dto;
using TwitterClone.Entity;
using TwitterClone.Service;
using TwitterCloneApplication.Models;


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
        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetPostList()
        {
            var list = new List<Post>();
            list = _postService.GetPostList();
            return Ok(list);
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

            // GetPost async değilse ve doğrudan Post dönüyorsa:
            //return GetPost(newPost.Id); // Eğer GetPost(int id) senkron çalışıyorsa

            // GetPost async ise ve Task<ActionResult<Post>> dönüyorsa:
            // Eğer GetPost(int id) asenkron çalışıyorsa
        }





    }
}
