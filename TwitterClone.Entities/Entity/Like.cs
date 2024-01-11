namespace TwitterClone.Entity;

public class Like
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public User User { get; set; } // Beğeni yapan kullanıcı
    public Post Post { get; set; } // Beğenilen post
}
