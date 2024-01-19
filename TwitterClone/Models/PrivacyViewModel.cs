using TwitterClone.Entity;
using TwitterClone.Dto;

namespace TwitterClone.Models
{
    public class PrivacyViewModel
    {
        public List<PostDto> Posts { get; set; }
        public int Repost { get; set; }
        public UserInformationDto User { get; set; }
        public bool IsFollowing { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }



    }
}
