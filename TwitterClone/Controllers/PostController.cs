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
        public async Task<ActionResult<CreatePostDto>> Create(CreatePostDto request, [FromServices] IPostService postService)
        {
            //IHttpContextAccessor httpContextAccessor;
            //var nameId = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier);
            //nameId.Value;
            {
                var result = await _postService.CreatePost(request);
                if(result != null)
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
}
