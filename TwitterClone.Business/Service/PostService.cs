
using Microsoft.EntityFrameworkCore;
using TwitterClone.Dto;
using TwitterClone.Entity;
using TwitterClone.Repository;


namespace TwitterClone.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
           
        }

        public async Task<PostDto> CreatePost(CreatePostDto request, int userId)
        {
            return await _postRepository.Create(request, userId);
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

        
        public async Task<RePost> RetweetPostAsync(int postId, int userId)
        {
            var rePost = new RePost
            {
                PostId = postId,
                UserId = userId,
            };

            return await _postRepository.AddRePostAsync(rePost);
        }

        public async Task<List<PostDto>> GetPostsByUserIdAsync(int userId)
        {
            return await _postRepository.GetPostsByUserIdAsync(userId);
        }

        public async Task<List<PostDto>> GetFeedForUserAsync(int userId)
        {
            return await _postRepository.GetFeedAsync(userId);
        }

    }
}
