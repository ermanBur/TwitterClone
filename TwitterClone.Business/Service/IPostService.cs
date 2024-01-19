using TwitterClone.Dto;
using TwitterClone.Entity;



namespace TwitterClone.Service
{
    public interface IPostService
    {
        Post GetPost(int id);
        Task DeletePostById(int id);

        Task<PostDto> CreatePost(CreatePostDto request, int userId);
        Task<RePost> RetweetPostAsync(int postId, int userId);
        Task<List<PostDto>> GetPostsByUserIdAsync(int userId);
        Task<List<PostDto>> GetFeedForUserAsync(int userId);
        Task<IEnumerable<Post>> SearchPostsByContentAsync(string searchQuery);



    }
}
