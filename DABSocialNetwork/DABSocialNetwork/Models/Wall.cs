using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Wall
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public List<Post> UserPosts { get; set; }
    }
}