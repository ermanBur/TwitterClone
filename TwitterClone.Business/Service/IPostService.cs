using TwitterClone.Dto;
using TwitterClone.Entity;



namespace TwitterClone.Service
{
    public interface IPostService
    {
        Post GetPost(int id);
        Task DeletePostById(int id);

        Task<PostDto> CreatePost(CreatePostDto request, int userId);
        Task<ServiceResponse<RePost>> RetweetPostAsync(int postId, int userId);
        Task<List<PostDto>> GetPostsByUserIdAsync(int userId);
        Task<List<PostDto>> GetFeedForUserAsync(int userId);
        Task<IEnumerable<Post>> SearchPostsByContentAsync(string searchQuery);
        Task<Like> LikePostAsync(int postId, int userId);
        Task<ServiceResponse<bool>> ToggleRetweetAsync(int postId, int userId);
        Task<IEnumerable<PostDto>> GetRetweetsByUserIdAsync(int userId);
        Task<List<PostDto>> GetRetweetsForUserAsync(int userId);
        Task<List<PostDto>> GetFeedWithRetweetsForUserAsync(int userId);






    }
}
