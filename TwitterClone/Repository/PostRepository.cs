using TwitterCloneApplication.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
