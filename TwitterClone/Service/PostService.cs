
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
