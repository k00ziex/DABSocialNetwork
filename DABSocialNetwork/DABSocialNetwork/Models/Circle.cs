using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class Circle
    {
        public ObjectId Id { get; set; }
        public User[] Users;
        public string NameOfCircle { get; set; }
        public User MyUser { get; set; }
    }
}