using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DABSocialNetwork.Models
{
    public class Feed
    {
        public ObjectId Id { get; set; }

        [BsonId]
        public ObjectId User_Id { get; set; }
        public Post[] Posts { get; set; }
    }
}