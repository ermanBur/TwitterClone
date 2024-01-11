public class PostDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime PostedOn { get; set; }
    public UserDto User { get; set; } // Post sahibi kullanıcının DTO'su
}
