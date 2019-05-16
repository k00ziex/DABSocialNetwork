using System.Collections.Generic;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Circle
    {
        public ObjectId Id { get; set; }
        public List<ObjectId> Users;
        public string NameOfCircle { get; set; }
    }
}