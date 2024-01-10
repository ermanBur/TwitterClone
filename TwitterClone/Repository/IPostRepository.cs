namespace TwitterClone.Repository
{
    public interface IPostRepository
    {
        Task DeletePostById(int id);
        Task AddPostById(int id);
        Task<List<Post>> GetAllPostAsync();
        Post GetPostById(int id);
    }
}
