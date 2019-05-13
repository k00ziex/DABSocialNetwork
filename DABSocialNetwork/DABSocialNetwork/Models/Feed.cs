using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Feed
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public Post[] Posts { get; set; }
    }
}