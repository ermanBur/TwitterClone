using System.ComponentModel.DataAnnotations;
using TwitterCloneApplication.Models;

namespace TwitterClone.Dto
{
    public class PostDto
    {
        
        public int Id { get; set; }
        
        public string Content { get; set; }
        
        public User User { get; set; } // Post sahibi kullanıcı
    }
}
