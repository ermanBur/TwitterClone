using TwitterClone.Entity;

namespace TwitterClone.Models
{
    public class PrivacyViewModel
    {
        public List<PostDto> Posts { get; set; }
        public int Repost { get; set; }
        public UserInformationDto User { get; set; }


    }
}
