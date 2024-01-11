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

    private readonly TwitterCloneContext _context;

    public UserService(TwitterCloneContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
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

    public async Task<IEnumerable<User>> GetUserFollowersAsync(int userId)
    {
        return await _context.Follows
                             .Where(f => f.FollowingId == userId)
                             .Select(f => f.Follower)
                             .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUserFollowingsAsync(int userId)
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
                return user; // Kullanıcı bilgileriyle birlikte kullanıcı nesnesini döndür
            }
        }

        return null; // Doğrulama başarısızsa veya kullanıcı bulunamazsa null döndür
    }
    public async Task<bool> ExistsUserAsync(string username, string email)
    {
        bool userExists = await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower() || u.Email.ToLower() == email.ToLower());
        return userExists;
    }
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
