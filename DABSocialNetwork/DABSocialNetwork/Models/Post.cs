using System;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Post
    {
        public ObjectId Id { get; set; }

        public string Text { get; set; }

        public string Image { get; set; }

        public DateTime TimeOfPosting { get; set; }

        public Comment[] Comments { get; set; }
    }
}