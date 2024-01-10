using TwitterClone.Dto;
using TwitterCloneApplication.Models;

namespace TwitterClone.Service
{
    public interface IPostService
    {
        List<Post> GetPostList();
        Post GetPost(int id);
        Task DeletePostById(int id);

        Task<PostDto> CreatePost(CreatePostDto request);
    }
}
