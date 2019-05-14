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

            db.DropCollection("User");
            db.DropCollection("Post");
            db.DropCollection("Comment");

            var user1 = new User() { Age = 20, Email = "FakeMail1", Gender = "Attack Helicopter", Name = "Zacher", MyCircles = new List<Circle>()};
       
            var user2 = new User() { Age = 21, Email = "FakeMail2", Gender = "Alpha Male", Name = "Tobi" };
            UserColl.InsertOne(user2);
            var user3 = new User() { Age = 22, Email = "FakeMail3", Gender = "Trebusjaeyye", Name = "Andy" };
            UserColl.InsertOne(user3);
            var user4 = new User() { Age = 23, Email = "FakeMail4", Gender = "Beta Male", Name = "Engholm" };
            UserColl.InsertOne(user4);

            
            var userlist = new List<User>();
            
            userlist.Add(user2);
            userlist.Add(user3);
            userlist.Add(user4);

            var circle = new Circle()
            {
                NameOfCircle = "This is the name of my circle",
                Users = userlist
            };

            user1.MyCircles.Add(circle);
            UserColl.InsertOne(user1);

            Console.WriteLine("Done");

            //var userGotten = UserColl.Find(a => a.Name == "Zacher").ToList();
            //foreach (var USER in userGotten)
            //{
            //    Console.WriteLine("{0}",USER.Gender);
            //}

            //try
            //{
            //    UserColl.FindOneAndUpdate(
            //        a => a.Name == "Zacher" && a.Email == "FakeMail2", 
            //        Builders<User>.Update.Set(e=> e.MyFeed, new Feed()
            //        {
                        
            //        }));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}
            //Console.WriteLine("Done");

        }
    }
}
