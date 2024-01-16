using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterClone.Entity;

public interface IUserService
{
    Task<User> GetUserAsync(int userId);
    Task CreateUserAsync(User user, string password);
    Task<bool> ExistsUserAsync(string username, string email);
    Task<IEnumerable<User>> GetUserFollowersAsync(int userId);
    Task<IEnumerable<User>> GetUserFollowingsAsync(int userId);
    //Task CreateUserAsync(User user);
    Task<User> ValidateUserAsync(string username, string password);
    Task DeleteUserAsync(int userId);
    Task<UserInformationDto> GetUserInformationAsync(int userId);



    // Diğer gerekli metodlar...
}
