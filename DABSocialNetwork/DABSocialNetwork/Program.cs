using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DABSocialNetwork.Models;
using MongoDB.Bson;
using MongoDB.Driver;

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

            //var user = new User() { Age = 99, Email = "FakeMail2", Gender = "Attack Helicopter", Name = "Zacher"};
            //UserColl.InsertOne(user);
            Console.WriteLine("Done");

            var userGotten = UserColl.Find(a => a.Name == "Zacher").ToList();
            foreach (var USER in userGotten)
            {
                Console.WriteLine("{0}",USER.Gender);
            }

            try
            {
                UserColl.FindOneAndUpdate(
                    a => a.Name == "Zacher" && a.Email == "FakeMail2", 
                    Builders<User>.Update.Set(e=> e.MyFeed, new Feed()
                    {
                        
                    }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Done");

        }
    }
}
