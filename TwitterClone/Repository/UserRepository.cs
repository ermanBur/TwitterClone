using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterCloneApplication.Models;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly TwitterCloneContext _context;

    public UserRepository(TwitterCloneContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<IEnumerable<User>> GetFollowersAsync(int userId)
    {
        throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetFollowingsAsync(int userId)
    {
        // Takip edilenleri almak için SQL sorgusu veya LINQ sorgusu yazınız.
        // Örnek:
        // return await _context.Followings.Where(f => f.FollowerId == userId).Select(f => f.User).ToListAsync();
        throw new System.NotImplementedException();
    }

    // ... Diğer metodlar...
}
