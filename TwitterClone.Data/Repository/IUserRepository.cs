using TwitterClone.Entity;
using TwitterClone.Dto;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int userId);
    Task<IEnumerable<User>> GetFollowersAsync(int userId);
    Task<IEnumerable<User>> GetFollowingsAsync(int userId);
    Task CreateUserAsync(User user, string password);
    Task<User> ValidateUserAsync(string username, string password);
    Task<bool> ExistsUserAsync(string username, string email);
    Task<UserInformationDto> GetUserInformationAsync(int userId);
    Task<User> GetByUsernameAsync(string username);
    Task FollowUserAsync(int followerId, int followingId);
    Task UnfollowUserAsync(int followerId, int followingId);
    Task<int> GetFollowersCountAsync(int userId);
    Task<int> GetFollowingsCountAsync(int userId);
    IEnumerable<User> SearchByQuery(string searchQuery);



}
