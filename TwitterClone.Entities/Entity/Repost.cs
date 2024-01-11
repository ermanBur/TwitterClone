namespace TwitterClone.Entity;

public class RePost
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } // RePost'u yapan kullanıcı
    public int PostId { get; set; }
    public Post Post { get; set; } // Yeniden paylaşılan post
}
