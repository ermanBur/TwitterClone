using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterCloneApplication.Models;

namespace TwitterCloneApplication.Service;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(int userId);
    Task<IEnumerable<User>> GetUserFollowersAsync(int userId);
    Task<IEnumerable<User>> GetUserFollowingsAsync(int userId);
    Task CreateUserAsync(User user);
    // Diğer gerekli metodlar...
}
