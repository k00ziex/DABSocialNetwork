using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Post
    {
        public ObjectId Id { get; set; }

        public ObjectId UserId { get; set; }


        public string Text { get; set; }

        public string Image { get; set; }

        public DateTime TimeOfPosting { get; set; }

        public List<Comment> Comments { get; set; }

        public ObjectId CircleId { get; set; }
    }
}