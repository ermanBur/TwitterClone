namespace TwitterClone.Service
{
    public interface IPostService
    {
        List<Post> GetPostList();
        Post GetPost(int id);
        Task DeletePostById(int id);
    }
}
