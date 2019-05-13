using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Wall
    {
        public ObjectId id { get; set; }
        public Post[] UserPosts { get; set; }

        public User User { get; set; }
    }
}