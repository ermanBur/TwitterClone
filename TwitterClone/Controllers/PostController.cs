using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Dto;
using TwitterClone.Service;
using TwitterCloneApplication.Models;

namespace TwitterClone.Controllers
{
    public class PostController : Controller
    {
        private readonly TwitterCloneContext _context;
        private readonly IPostService _postService;

        public PostController(TwitterCloneContext context, IPostService postService) 
        {
            _context = context;
            _postService = postService;
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
        public async Task<ActionResult<CreatePostDto>> Create(CreatePostDto request)
        {
            
            {
                var user = await _context.Users.FindAsync(request.UserId);
                if (user == null)
                    return NotFound();

                var newPost = new Post
                {
                    Content = request.Content,
                    User = user,
                    PostedOn = DateTime.Now,
                };

                _context.Posts.Add(newPost);
                await _context.SaveChangesAsync();

                // GetPost async değilse ve doğrudan Post dönüyorsa:
                //return GetPost(newPost.Id); // Eğer GetPost(int id) senkron çalışıyorsa

                // GetPost async ise ve Task<ActionResult<Post>> dönüyorsa:
                return RedirectToAction("Index", "Home"); // Eğer GetPost(int id) asenkron çalışıyorsa
            }
            
        }

    }
}
