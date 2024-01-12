
using Microsoft.EntityFrameworkCore;
using TwitterClone.Entity;
using TwitterClone.Dto;
using TwitterClone.Contexts;

namespace TwitterClone.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly TwitterCloneContext _context;
        public PostRepository(TwitterCloneContext context) 
        {
            _context = context;
        }
        public async Task AddPostById(int id)
        {
            _context.Posts.Add(new Post { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task<PostDto> Create(CreatePostDto request, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
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
            var postToDelete = await _context.Posts.FindAsync(id);
            if (postToDelete != null) 
            {
                 _context.Posts.Remove(postToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Post>> GetAllPostAsync()
        {
            var tweets = _context.Posts.Include(c =>c.User).OrderByDescending(t => t.PostedOn).ToList();
            return tweets;
            //return await _context.Posts.Include(c => c.User).ToListAsync();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.Find(id); 
        }

        public async Task<List<PostDto>> GetPostsByUserIdAsync(int userId)
        {
            var posts = await _context.Posts
                                      .Where(p => p.UserId == userId)
                                      .Select(post => new PostDto
                                      {
                                          Id = post.Id,
                                          Content = post.Content,
                                          PostedOn = post.PostedOn,
                                          User = new UserDto
                                          {
                                              Id = post.User.Id,
                                              Username = post.User.Username
                                          }
                                      }).ToListAsync();

            return posts;
        }




        public async Task<RePost> AddRePostAsync(RePost rePost)
        {
            await _context.RePosts.AddAsync(rePost);
            await _context.SaveChangesAsync();
            return rePost;
        }
    }
}
