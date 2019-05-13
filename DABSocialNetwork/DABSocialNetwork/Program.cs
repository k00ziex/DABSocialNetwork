using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DABSocialNetwork.Models;

namespace DABSocialNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            var client = new MongoDB.Driver.MongoClient();
            var db = client.GetDatabase("DABMandatory3");
            var UserColl = db.GetCollection<User>("User");
            var PostColl = db.GetCollection<Post>("Post");
            var CommentColl = db.GetCollection<Comment>("Comment");

            var user = new User(){Age = 18, Email = "FakeMail", Gender = "Male", Name = "Andy"};
            UserColl.InsertOne(user);
            Console.WriteLine("Done");
        }
    }
}
