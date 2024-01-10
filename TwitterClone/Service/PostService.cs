
using TwitterClone.Dto;
using TwitterClone.Repository;
using TwitterCloneApplication.Models;

namespace TwitterClone.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly TwitterCloneContext _context;

        public PostService(IPostRepository postRepository, TwitterCloneContext context)
        {
            _postRepository = postRepository;
            _context = context;
        }

        public async Task<PostDto> CreatePost(CreatePostDto request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return null; 

            var newPost = new Post
            {
                Content = request.Content,
                User = user,
                PostedOn = DateTime.Now,
            };

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            
            var postDto = new PostDto
            {
                
                Id = newPost.Id,
                Content = newPost.Content,
                
            };

            return postDto;
        }
    

            public async Task DeletePostById(int id)
        {
            var postToDelete = _postRepository.GetPostById(id);
            if (postToDelete != null)
                await _postRepository.DeletePostById(id);
        }

        public Post GetPost(int id)
        {
            return _postRepository.GetPostById(id);
        }

        public List<Post> GetPostList()
        {
            return _postRepository.GetAllPostAsync().Result;
        }

        
    }
}
