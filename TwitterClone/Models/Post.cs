using TwitterCloneApplication.Models;

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime PostedOn { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } // Post sahibi kullanıcı
    public ICollection<Like> Likes { get; set; } = new List<Like>(); // Post'a yapılan beğeniler
    public ICollection<RePost> RePosts { get; set; } // Post'un repostları

}
