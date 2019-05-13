using System;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Post
    {
        public ObjectId Id { get; set; }

        public string PostContent { get; set; }

        public DateTime TimeOfPosting { get; set; }

        public Comment[] Comments { get; set; }
    }
}