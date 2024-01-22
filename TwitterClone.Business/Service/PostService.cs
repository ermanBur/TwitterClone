
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


        public async Task<ServiceResponse<RePost>> RetweetPostAsync(int postId, int userId)
        {
            if (await _postRepository.HasUserAlreadyRePosted(postId, userId))
            {
                return new ServiceResponse<RePost>
                {
                    Success = false,
                    ErrorMessage = "You have already retweeted this post."
                };
            }

            var rePost = new RePost
            {
                PostId = postId,
                UserId = userId,
                // CreatedAt alanı otomatik olarak set edilecek
            };

            await _postRepository.AddRePostAsync(rePost);

            return new ServiceResponse<RePost>
            {
                Data = rePost,
                Success = true
            };
        }



        public async Task<List<PostDto>> GetPostsByUserIdAsync(int userId)
        {
            return await _postRepository.GetPostsByUserIdAsync(userId);
        }

        public async Task<List<PostDto>> GetFeedForUserAsync(int userId)
        {
            return await _postRepository.GetFeedAsync(userId);

        }
        public async Task<List<PostDto>> GetFeedWithRetweetsForUserAsync(int userId)
        {
            var feedPosts = await _postRepository.GetFeedAsync(userId);
            var retweets = await _postRepository.GetRetweetsByUserIdAsync(userId);

            // Feed postları ve retweet'leri birleştirip, zamanlarına göre sırala
            var feedWithRetweets = feedPosts.Concat(retweets)
                                            .OrderByDescending(p => p.IsRetweet ? p.RetweetTime : p.PostedOn)
                                            .ToList();

            return feedWithRetweets;
        }





        public async Task<IEnumerable<Post>> SearchPostsByContentAsync(string searchQuery)
        {
            var posts = await _postRepository.SearchPostsByContentAsync(searchQuery);
            var result = posts.Select(post => new PostDto
            {
                Id = post.Id,
                Content = post.Content,
            });
            return posts;
        }

        public async Task<Like> LikePostAsync(int postId, int userId)
        {
            var like = new Like
            {
                PostId = postId,
                UserId = userId,
            };

            return await _postRepository.AddLikePostAsync(like);
        }

        public async Task<ServiceResponse<bool>> ToggleRetweetAsync(int postId, int userId)
        {
            try
            {
                var didRetweet = await _postRepository.ToggleRetweetAsync(postId, userId);
                return new ServiceResponse<bool>
                {
                    Data = didRetweet,
                    Success = true,
                    Message = didRetweet ? "Retweeted successfully." : "Retweet removed successfully."
                };
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<IEnumerable<PostDto>> GetRetweetsByUserIdAsync(int userId)
        {
            return await _postRepository.GetRetweetsByUserIdAsync(userId);
        }

        public Task<List<PostDto>> GetRetweetsForUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
