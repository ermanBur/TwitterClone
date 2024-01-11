using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TwitterClone.Dto;
using TwitterClone.Service;
using TwitterCloneApplication.Models;

namespace TwitterClone.Controllers
{
    public class PostController : Controller
    {
        private readonly TwitterCloneContext _context;
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PostController(TwitterCloneContext context, IPostService postService, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
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
            if(id != null) 
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

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var newPost = new Post
            {
                Content = createPostDto.Content,
                UserId = userId, // UserId'yi doğrudan atayın
                PostedOn = DateTime.Now
            };

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            var newPostDto = new PostDto
            {
                Id = newPost.Id,
                Content = newPost.Content,
                PostedOn = newPost.PostedOn,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username
                }
            };

            return RedirectToAction("Index", "Home");
        }



    }
}
