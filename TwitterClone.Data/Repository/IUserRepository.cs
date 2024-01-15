using TwitterClone.Entity;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int userId);
    Task<IEnumerable<User>> GetFollowersAsync(int userId);
    Task<IEnumerable<User>> GetFollowingsAsync(int userId);
    Task CreateUserAsync(User user, string password);
    Task<User> ValidateUserAsync(string username, string password);
    Task<bool> ExistsUserAsync(string username, string email);
    Task<UserInformationDto> GetUserInformationAsync(int userId);


    // Diğer gerekli metodlar...
}
