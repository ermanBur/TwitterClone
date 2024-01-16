using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using TwitterClone.Contexts;
using TwitterClone.Entity;

public class UserRepository : IUserRepository
{
    private readonly TwitterCloneContext _context;

    public UserRepository(TwitterCloneContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user, string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        user.PasswordHash = hashed;
        user.Salt = Convert.ToBase64String(salt);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsUserAsync(string username, string email)
    {
        bool userExists = await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower() || u.Email.ToLower() == email.ToLower());
        return userExists;
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<IEnumerable<User>> GetFollowersAsync(int userId)
    {
        return await _context.Follows
                             .Where(f => f.FollowingId == userId)
                             .Select(f => f.Follower)
                             .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetFollowingsAsync(int userId)
    {
        return await _context.Follows
                             .Where(f => f.FollowerId == userId)
                             .Select(f => f.Following)
                             .ToListAsync();
    }

    public async Task<User> ValidateUserAsync(string username, string password)
    {
        var user = await _context.Users
                                 .FirstOrDefaultAsync(u => u.Username == username);

        if (user != null)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(user.Salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (hashed == user.PasswordHash)
            {
                return user; 
            }
        }

        return null; 
    }
    public async Task<UserInformationDto> GetUserInformationAsync(int userId)
    {
        var user = await _context.Users
                                 .Where(u => u.Id == userId)
                                 .Select(u => new UserInformationDto
                                 {
                                     Username = u.Username,
                                     Email = u.Email,
                                     PostCount = u.Posts.Count,
                                     LikesCount = u.Likes.Count,
                                     MessagesCount = u.MessagesSent.Count + u.MessagesReceived.Count,
                                     JoinedDate = u.JoinDate

                                 }).FirstOrDefaultAsync();

        return user;
    }
    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
                             .AsNoTracking()
                             .FirstOrDefaultAsync(u => EF.Functions.Like(u.Username, username));
    }

}
