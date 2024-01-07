using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterCloneApplication.Models;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(int userId);
    Task CreateUserAsync(User user, string password);

    Task<IEnumerable<User>> GetUserFollowersAsync(int userId);
    Task<IEnumerable<User>> GetUserFollowingsAsync(int userId);
    //Task CreateUserAsync(User user);
    Task<bool> ValidateUserAsync(string username, string password);
    Task DeleteUserAsync(int userId);


    // Diğer gerekli metodlar...
}
