using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using TwitterClone.Entity;
using TwitterClone.Contexts;
using TwitterClone.Repository;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly TwitterCloneContext _context;
    private readonly IPostRepository _postRepository;

    public UserService(TwitterCloneContext context, IUserRepository userRepository, IPostRepository postRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public async Task<User> GetUserAsync(int userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task CreateUserAsync(User user, string password)
    {
         await _userRepository.CreateUserAsync(user, password);
    }

    public async Task<IEnumerable<User>> GetUserFollowersAsync(int userId)
    {
        return await _userRepository.GetFollowersAsync(userId);
    }

    public async Task<IEnumerable<User>> GetUserFollowingsAsync(int userId)
    {
        return await _userRepository.GetFollowingsAsync(userId);
    }

    public Task<User> ValidateUserAsync(string username, string password)
    {
        return _userRepository.ValidateUserAsync(username, password);
    }
    public Task<bool> ExistsUserAsync(string username, string email)
    {
        return _userRepository.ExistsUserAsync(username, email);
    }
    public async Task<UserInformationDto> GetUserInformationAsync(int userId)
    {
        return await _userRepository.GetUserInformationAsync(userId);
    }

    //Şimdilik çalışmıyor sonrasında düzenlenme yapılarak _context kısmı ile kaldırılıp repo tanımlanacak
    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<UserInformationDto> GetUserInformationByUsernameAsync(string username, int currentUserId)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            return null;
        }

        var posts = await _postRepository.GetPostsByUserIdAsync(user.Id);
        var isFollowing = await _context.Follows.AnyAsync(f => f.FollowerId == currentUserId && f.FollowingId == user.Id);


        var userInformationDto = new UserInformationDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            PostCount = posts.Count,
            LikesCount = user.Likes.Count,
            MessagesCount = user.MessagesSent.Count + user.MessagesReceived.Count,
            JoinedDate = user.JoinDate,
            IsFollowing = isFollowing,
            Posts = posts.Select(post => new PostDto
            {
                Id = post.Id,
                Username = user.Username,
                Content = post.Content,
                PostedOn = post.PostedOn
            }).ToList(),

        };

        return userInformationDto;

    }
    public async Task FollowUserAsync(int followerId, int followingId)
    {
        await _userRepository.FollowUserAsync(followerId, followingId);
    }

    public async Task UnfollowUserAsync(int followerId, int followingId)
    {
        await _userRepository.UnfollowUserAsync(followerId, followingId);
    }
    public async Task<int> GetUserFollowersCountAsync(int userId)
    {
        return await _context.Follows.CountAsync(f => f.FollowingId == userId);
    }

    public async Task<int> GetUserFollowingsCountAsync(int userId)
    {
        return await _context.Follows.CountAsync(f => f.FollowerId == userId);
    }

    public async Task<bool> IsFollowingAsync(int currentUserId, int targetUserId)
    {
        return await _context.Follows.AnyAsync(f => f.FollowerId == currentUserId && f.FollowingId == targetUserId);
    }

}
