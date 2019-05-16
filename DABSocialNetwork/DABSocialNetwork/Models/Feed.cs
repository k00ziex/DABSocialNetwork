using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DABSocialNetwork.Models
{
    public class Feed
    {
        public ObjectId Id { get; set; }
        public ObjectId User_Id { get; set; }
        public List<Post> Posts { get; set; }
    }
}