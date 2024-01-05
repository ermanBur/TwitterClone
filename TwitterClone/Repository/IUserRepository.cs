using TwitterCloneApplication.Models;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int userId);
    Task<IEnumerable<User>> GetFollowersAsync(int userId);
    Task<IEnumerable<User>> GetFollowingsAsync(int userId);
    // Diğer gerekli metodlar...
}
