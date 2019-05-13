using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Circle
    {
        public ObjectId Id { get; set; }
        public User[] Users;
        public User MyUser { get; set; }
    }
}