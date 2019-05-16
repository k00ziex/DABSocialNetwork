﻿using System;
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


            /// TODO OPRETTELSE AF USERS
            var user1 = new User() { Age = 20, Email = "FakeMail1", Gender = "Attack Helicopter", Name = "Zacher", MyCircles = new List<Circle>()};
            var user2 = new User() { Age = 21, Email = "FakeMail2", Gender = "Alpha Male", Name = "Tobi" };
            var user3 = new User() { Age = 22, Email = "FakeMail3", Gender = "Trebusjaeyye", Name = "Andy" };
            var user4 = new User() { Age = 23, Email = "FakeMail4", Gender = "Beta Male", Name = "Engholm" };

            /// TODO OPRETTELSE AF WALLS
            var wallUser1 = new Wall(){UserId = user1.Id };
            var wallUser2 = new Wall() { UserId = user2.Id };
            var wallUser3 = new Wall() { UserId = user3.Id };
            var wallUser4 = new Wall() { UserId = user4.Id };

            user1.MyWall = wallUser1;
            user2.MyWall = wallUser2;
            user3.MyWall = wallUser3;
            user4.MyWall = wallUser4;

            /// TODO OPRETTELSE AF FEEDS
            var feedUser1 = new Feed() { User_Id = user1.Id };
            var feedUser2 = new Feed() { User_Id = user2.Id };
            var feedUser3 = new Feed() { User_Id = user3.Id };
            var feedUser4 = new Feed() { User_Id = user4.Id };

            user1.MyFeed = feedUser1;
            user2.MyFeed = feedUser2;
            user3.MyFeed = feedUser3;
            user4.MyFeed = feedUser4;

            /// TODO OPRETTELSE AF POSTS
            var postUser1_1 = new Post()
            {
                Image = "Picture of a chicken",
                UserId = user1.Id,
                Text = "This is a beautiful cock",
                TimeOfPosting = new DateTime(2019, 05, 16),
            };
            var postUser1_2 = new Post()
            {
                Image = "Picture of a cow",
                UserId = user1.Id,
                Text = "This is a weird milk machine",
                TimeOfPosting = new DateTime(2019, 05, 04),
            };

            var postUser2_1 = new Post()
            {
                Image = "Motivational picture",
                UserId = user1.Id,
                Text = "You can do it!",
                TimeOfPosting = new DateTime(2019, 05, 13),
            };
            var postUser2_2 = new Post()
            {
                Image = "Workout image",
                UserId = user1.Id,
                Text = "Getting fit today #Workout #Gym #FitLife",
                TimeOfPosting = new DateTime(2019, 05, 16),
            };

            var postUser3_1 = new Post()
            {
                Image = "Picture of climbing Mount Everest",
                UserId = user1.Id,
                Text = "Finally did it! #Climbing #FuckYeah",
                TimeOfPosting = new DateTime(2019, 2, 3),
            };
            var postUser3_2 = new Post()
            {
                Image = "Picture of Darth Vader",
                UserId = user1.Id,
                Text = "May the fourth be with you!",
                TimeOfPosting = new DateTime(2019, 5, 4),
            };

            var postUser4_1 = new Post()
            {
                Image = "Picture of a furry",
                UserId = user1.Id,
                Text = "Wouldn't I look cute like this? #Cuteness #Furry4Life #HarderDaddy",
                TimeOfPosting = new DateTime(2019, 05, 12),
            };

            var postUser4_2 = new Post()
            {
                Image = "Picture of starving children",
                UserId = user1.Id,
                Text = "I'm not racist, I'm just afraid of the dark.",
                TimeOfPosting = new DateTime(2019, 05, 18),
            };

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
