using System;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Wall
    {
        public ObjectId Id { get; set; }
        public Object UserId { get; set; }
        public Post[] UserPosts { get; set; }

        public User User { get; set; }
    }
}