
namespace TwitterClone.Entity;
public class Follow
{
    public int FollowerId { get; set; }
    public User Follower { get; set; }

    public int FollowingId { get; set; }
    public User Following { get; set; }
    
}
