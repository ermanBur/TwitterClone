public class UserInformationDto
{
    public string Username { get; set; }
    public string Email { get; set; } 
    public int PostCount { get; set; } 
    public int LikesCount { get; set; }
    public int MessagesCount { get; set; }
    public DateTime JoinedDate { get; set; }

    // Diğer bilgiler...
}
