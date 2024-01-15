using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using TwitterClone.Entity;
using TwitterClone.Contexts;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly TwitterCloneContext _context;

    public UserService(TwitterCloneContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
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


}
