using System;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Comment
    {
        public ObjectId Id { get; set; }

        public DateTime TimeOfCommenting { get; set; }

        public string CommentContent { get; set; }
        
        //Bør bruges til at sætte navn på hvem der har kommenteret, men ikke ifølge opgavebeskrivelsen eller hvad?
        public ObjectId User_Id { get; set; }

        public ObjectId PostId { get; set; }
    }
}