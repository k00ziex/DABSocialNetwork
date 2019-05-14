using System.Collections.Generic;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class BlockedUsers
    {
        public ObjectId Id { get; set; }

        public string NameOfList { get; set; }

        public List<User> MyBlockedUsers { get; set; }
        public User MyUser { get; set; }
    }
}