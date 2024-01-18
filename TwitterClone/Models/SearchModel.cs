using TwitterClone.Dto;
using TwitterClone.Entity;

namespace TwitterClone.Models
{
    public class SearchModel
    {
        public string SearchQuery { get; set; }
        public List<UserInformationDto> SearchResults { get; set; }
        public List<UserDto> Users { get; set; }
        public List<Post> PostSearch { get; set; }
        

    }
}
