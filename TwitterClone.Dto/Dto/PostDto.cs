using TwitterClone.Dto;
public class PostDto
{
    public int Id { get; set; }
    public string Username { get; set; }

    public string Content { get; set; }
    public DateTime PostedOn { get; set; }
    public UserDto User { get; set; }
    public int RePosts { get; set; }
    public int Likes { get; set; }

}
