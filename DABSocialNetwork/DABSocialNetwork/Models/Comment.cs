using System;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Comment
    {
        public ObjectId Id { get; set; }

        public DateTime TimeOfCommenting { get; set; }

        public string CommentContent { get; set; }

        public User User { get; set; }

    }
}