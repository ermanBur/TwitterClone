using TwitterCloneApplication.Models;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int SenderId { get; set; }
    public User Sender { get; set; } // Mesajı gönderen kullanıcı
    public int ReceiverId { get; set; }
    public User Receiver { get; set; } // Mesajı alan kullanıcı
}
