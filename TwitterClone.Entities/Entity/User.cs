using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Entity;
public class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
    public string Username { get; set; }
    //public string DisplayName { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(255, ErrorMessage = "Email address must not exceed 255 characters.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    //public string Password { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;





    public ICollection<Post> Posts { get; set; } = new List<Post>(); // Kullanıcının postları
    public ICollection<Like> Likes { get; set; } = new List<Like>(); // Kullanıcının beğenileri
    public ICollection<Message> MessagesSent { get; set; } = new List<Message>(); // Kullanıcının gönderdiği mesajlar
    public ICollection<Message> MessagesReceived { get; set; } = new List<Message>(); // Kullanıcının aldığı mesajlar
    public ICollection<RePost> RePosts { get; set; } // Kullanıcının repostları

}
