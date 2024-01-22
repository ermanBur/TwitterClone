using TwitterClone.Dto;
using TwitterClone.Entity;

namespace TwitterClone.Repository
{
    public interface IPostRepository
    {
        Task DeletePostById(int id);
        Task AddPostById(int id);
        Task<List<Post>> GetAllPostAsync();
        Post GetPostById(int id);

        Task<PostDto> Create(CreatePostDto request, int userId);
        Task<RePost> AddRePostAsync(RePost rePost);
        Task<bool> HasUserAlreadyRePosted(int postId, int userId);

        Task<List<PostDto>> GetPostsByUserIdAsync(int userId);
        Task<List<PostDto>> GetFeedAsync(int userId);
        Task<IEnumerable<Post>> SearchPostsByContentAsync(string searchQuery);
        Task<bool> ToggleRetweetAsync(int postId, int userId);
        Task<IEnumerable<PostDto>> GetRetweetsByUserIdAsync(int userId);



        Task<Like> AddLikePostAsync(Like like);


    }
}
