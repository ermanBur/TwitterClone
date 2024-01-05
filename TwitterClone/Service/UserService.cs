using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterCloneApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace TwitterCloneApplication.Service;

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

    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUserFollowersAsync(int userId)
    {
        // Bu metod, takipçileri döndürmek için veritabanınızın yapısına göre uygulanmalıdır.
        throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetUserFollowingsAsync(int userId)
    {
        // Bu metod, takip edilen kullanıcıları döndürmek için veritabanınızın yapısına göre uygulanmalıdır.
        throw new System.NotImplementedException();
    }

    // ... Diğer gerekli metodlar...
}
