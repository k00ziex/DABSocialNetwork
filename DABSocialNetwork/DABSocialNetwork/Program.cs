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
            Console.WriteLine("Welcome to the MongoDB assignment from Group 6!");
            Console.WriteLine("_____________________________________________________");

            Console.WriteLine("Please enter the name of the database you want created, or leave it blank (Then Group6_DAB_Assignment will be created)");
            var dbname = Console.ReadLine();
            var query = new Queries(dbname);
            var create = new Creation(dbname);

            Dataseeding(dbname);

            string keyRead;
            do
            {
                Console.WriteLine("\n\n\nPlease select one of the following options:");
                Console.WriteLine("Press 1 for creating a post.");
                Console.WriteLine("Press 2 for creating a comment.");
                Console.WriteLine("Press 3 for seeing a users feed.");
                Console.WriteLine("Press 4 for seeing a users wall.");
                Console.WriteLine("Press Q for quitting the application.");

                keyRead = Console.ReadKey().KeyChar.ToString().ToLower();

                switch (keyRead)
                {
                    case "1":
                        Console.WriteLine("Please enter the ID of the owner of the post you're now creating (Check the MongoDB if you don't know it).");
                        var idOfPost = Console.ReadLine();
                        var postID = new ObjectId(idOfPost);
                        Console.WriteLine("Please enter the image you want to include.");
                        var imageOfPost = Console.ReadLine();
                        Console.WriteLine("Please enter the text of the post.");
                        var textOfPost = Console.ReadLine();
                        Console.WriteLine("Please enter the name of the circle you want to post it in (As long as you're part of it).");
                        var circleNameOfPost = Console.ReadLine();
                        create.CreatePost(postID, imageOfPost, textOfPost,circleNameOfPost);
                        break;

                    case "2":
                        Console.WriteLine("Please enter the ID of the post you'd like to comment on.");
                        var idOfPostToComment = Console.ReadLine();
                        var postIdToComment = new ObjectId(idOfPostToComment);
                        Console.WriteLine("Please enter the content for the comment.");
                        var content = Console.ReadLine();
                        create.CreateComment(postIdToComment, content);
                        break;

                    case "3":
                        Console.WriteLine("Please enter the ID of the user who you want to see a feed for.");
                        var usersNameID = Console.ReadLine();
                        var userToSeeFeedFor = new ObjectId(usersNameID);
                        query.Feed(userToSeeFeedFor);
                        break;

                    case "4":
                        Console.WriteLine("Please enter the ID of the user you want to visit");
                        var userToVisitID = Console.ReadLine();
                        var userToVisit = new ObjectId(userToVisitID);
                        Console.WriteLine("Please enter the ID of the visiting user");
                        var userVisitingID = Console.ReadLine();
                        var userVisiting = new ObjectId(userVisitingID);
                        query.Wall(userToVisit, userVisiting);
                        break;

                    case "q":
                        Console.WriteLine("Exiting the application");
                        break;

                    default:
                        Console.WriteLine("*****Invalid entry! Try again!*****");
                        break;
                }

            } while (keyRead != "q");
        }


        static void Dataseeding(string databasename = "Group6_DAB_Assignment")
        {
            var client = new MongoDB.Driver.MongoClient();
            var db = client.GetDatabase(databasename);
            var UserColl = db.GetCollection<User>("User");
            var PostColl = db.GetCollection<Post>("Post");
            var CommentColl = db.GetCollection<Comment>("Comment");
            var FeedColl = db.GetCollection<Feed>("Feed");
            var BlockedColl = db.GetCollection<BlockedUsers>("BlockedUsers");

            db.DropCollection("User");
            db.DropCollection("Post");
            db.DropCollection("Comment");
            db.DropCollection("Feed");
            db.DropCollection("BlockedUsers");


            /// TODO OPRETTELSE AF USERS
            var user1 = new User()
            {
                Age = 20,
                Email = "FakeMail1",
                Gender = "Attack Helicopter",
                Name = "Zacher",
                Id = ObjectId.GenerateNewId()
            };
            var user2 = new User()
            {
                Age = 21,
                Email = "FakeMail2",
                Gender = "Alpha Male",
                Name = "Tobi",
                Id = ObjectId.GenerateNewId()
            };
            var user3 = new User()
            {
                Age = 22,
                Email = "FakeMail3",
                Gender = "Trebusjaeyye",
                Name = "Andy",
                Id = ObjectId.GenerateNewId()
            };
            var user4 = new User()
            {
                Age = 23,
                Email = "FakeMail4",
                Gender = "Beta Male",
                Name = "Engholm",
                Id = ObjectId.GenerateNewId()
            };

            UserColl.InsertOne(user1);
            UserColl.InsertOne(user2);
            UserColl.InsertOne(user3);
            UserColl.InsertOne(user4);

            /// TODO OPRETTELSE AF CIRCLES
            var circle1User1 = new Circle() { NameOfCircle = "My Bestest Friends", Users = new List<ObjectId>() };
            var circle2User1 = new Circle() { NameOfCircle = "Everyone I Love", Users = new List<ObjectId>() };
            var circle1User3 = new Circle() { NameOfCircle = "Watch This Shit", Users = new List<ObjectId>() };

            circle1User1.Users.Add(user2.Id);
            circle1User1.Users.Add(user3.Id);
            circle1User1.Users.Add(user4.Id);

            circle2User1.Users.Add(user2.Id);

            circle1User3.Users.Add(user1.Id);

            /// TODO OPRETTELSE AF BLOCKEDUSERS
            var blockedListUser1 = new BlockedUsers() { NameOfList = "Zachers blocks", MyBlockedUsers = new List<ObjectId>() { user2.Id } };

            user1.MyBlockedUsersId = blockedListUser1.Id;

            /// TODO OPRETTELSE AF WALLS
            var wallUser1 = new Wall() { UserId = user1.Id, UserPosts = new List<Post>() };
            var wallUser2 = new Wall() { UserId = user2.Id, UserPosts = new List<Post>() };
            var wallUser3 = new Wall() { UserId = user3.Id, UserPosts = new List<Post>() };
            var wallUser4 = new Wall() { UserId = user4.Id, UserPosts = new List<Post>() };

            user1.MyWall = wallUser1;
            user2.MyWall = wallUser2;
            user3.MyWall = wallUser3;
            user4.MyWall = wallUser4;

            /// TODO OPRETTELSE AF FEEDS
            var feedUser1 = new Feed() { User_Id = user1.Id, Posts = new List<Post>() };
            var feedUser2 = new Feed() { User_Id = user2.Id, Posts = new List<Post>() };
            var feedUser3 = new Feed() { User_Id = user3.Id, Posts = new List<Post>() };
            var feedUser4 = new Feed() { User_Id = user4.Id, Posts = new List<Post>() };

            /// TODO OPRETTELSE AF POSTS
            var postUser1_1 = new Post()
            {
                Image = "Picture of a chicken",
                UserId = user1.Id,
                Text = "This is a beautiful cock",
                TimeOfPosting = new DateTime(2019, 05, 16),
                CircleName = circle1User1.NameOfCircle,
                Comments = new List<Comment>(),
                Id = ObjectId.GenerateNewId()

            };
            var postUser1_2 = new Post()
            {
                Image = "Picture of a cow",
                UserId = user1.Id,
                Text = "This is a weird milk machine",
                TimeOfPosting = new DateTime(2019, 05, 04),
                Comments = new List<Comment>(),
                Id = ObjectId.GenerateNewId()

            };

            var postUser2_1 = new Post()
            {
                Image = "Motivational picture",
                UserId = user1.Id,
                Text = "You can do it!",
                TimeOfPosting = new DateTime(2019, 05, 13),
                Comments = new List<Comment>(),
                Id = ObjectId.GenerateNewId()

            };
            var postUser2_2 = new Post()
            {
                Image = "Workout image",
                UserId = user1.Id,
                Text = "Getting fit today #Workout #Gym #FitLife",
                TimeOfPosting = new DateTime(2019, 05, 16),
                Comments = new List<Comment>(),
                Id = ObjectId.GenerateNewId()

            };

            var postUser3_1 = new Post()
            {
                Image = "Picture of climbing Mount Everest",
                UserId = user1.Id,
                Text = "Finally did it! #Climbing #FuckYeah",
                TimeOfPosting = new DateTime(2019, 2, 3),
                Comments = new List<Comment>(),
                Id = ObjectId.GenerateNewId()

            };

            var postUser3_2 = new Post()
            {
                Image = "Picture of Darth Vader",
                UserId = user1.Id,
                Text = "May the fourth be with you!",
                TimeOfPosting = new DateTime(2019, 5, 4),
                CircleName = circle1User3.NameOfCircle,
                Comments = new List<Comment>(),
                Id = ObjectId.GenerateNewId()

            };

            var postUser4_1 = new Post()
            {
                Image = "Picture of a furry",
                UserId = user1.Id,
                Text = "Wouldn't I look cute like this? #Cuteness #Furry4Life #HarderDaddy",
                TimeOfPosting = new DateTime(2019, 05, 12),
                Comments = new List<Comment>(),
                Id = ObjectId.GenerateNewId()

            };

            var postUser4_2 = new Post()
            {
                Image = "Picture of himself in a furry costume",
                UserId = user1.Id,
                Text = "Felt cute. Might delete later <3",
                TimeOfPosting = new DateTime(2019, 05, 18),
                Comments = new List<Comment>(),
                Id = ObjectId.GenerateNewId()

            };

            /// TODO OPRETTELSE AF COMMENTS
            var commentUser2 = new Comment()
            {
                CommentContent = "The fuck is this?!?!",
                PostId = postUser4_2.Id,
                TimeOfCommenting = new DateTime(2019, 05, 18, 12, 32, 11),
                User_Id = user2.Id,
                Id = ObjectId.GenerateNewId()
            };
            var commentUser3 = new Comment()
            {
                CommentContent = "Go get some help ffs",
                PostId = postUser4_2.Id,
                TimeOfCommenting = new DateTime(2019, 05, 18, 12, 45, 32),
                User_Id = user3.Id,
                Id = ObjectId.GenerateNewId()

            };

            var commentUser1 = new Comment()
            {
                CommentContent = "You go girl!",
                PostId = postUser2_2.Id,
                TimeOfCommenting = new DateTime(2019, 5, 17, 15, 31, 9),
                User_Id = user1.Id,
                Id = ObjectId.GenerateNewId()

            };

            var commentUser4 = new Comment()
            {
                CommentContent = "Work it baby! <3",
                PostId = postUser2_2.Id,
                TimeOfCommenting = new DateTime(2019, 5, 27, 8, 13, 7),
                User_Id = user4.Id,
                Id = ObjectId.GenerateNewId()

            };



            postUser4_2.Comments.Add(commentUser2);
            postUser4_2.Comments.Add(commentUser3);
            postUser2_2.Comments.Add(commentUser1);
            postUser2_2.Comments.Add(commentUser4);

            wallUser1.UserPosts.Add(postUser1_1);
            wallUser1.UserPosts.Add(postUser1_2);
            wallUser2.UserPosts.Add(postUser2_1);
            wallUser2.UserPosts.Add(postUser2_2);
            wallUser3.UserPosts.Add(postUser3_1);
            wallUser3.UserPosts.Add(postUser3_2);
            wallUser4.UserPosts.Add(postUser4_1);
            wallUser4.UserPosts.Add(postUser4_2);


            //Feeds
            feedUser1.Posts.Add(postUser1_1);
            feedUser2.Posts.Add(postUser1_1);
            feedUser3.Posts.Add(postUser1_1);
            feedUser4.Posts.Add(postUser1_1);

            feedUser1.Posts.Add(postUser1_2);
            feedUser2.Posts.Add(postUser1_2);
            feedUser3.Posts.Add(postUser1_2);
            feedUser4.Posts.Add(postUser1_2);


            feedUser2.Posts.Add(postUser2_1);
            feedUser3.Posts.Add(postUser2_1);
            feedUser4.Posts.Add(postUser2_1);


            feedUser2.Posts.Add(postUser2_2);
            feedUser3.Posts.Add(postUser2_2);
            feedUser4.Posts.Add(postUser2_2);

            feedUser1.Posts.Add(postUser3_1);
            feedUser2.Posts.Add(postUser3_1);
            feedUser3.Posts.Add(postUser3_1);
            feedUser4.Posts.Add(postUser3_1);

            feedUser1.Posts.Add(postUser3_2);
            feedUser3.Posts.Add(postUser3_2);

            feedUser1.Posts.Add(postUser4_1);
            feedUser2.Posts.Add(postUser4_1);
            feedUser3.Posts.Add(postUser4_1);
            feedUser4.Posts.Add(postUser4_1);

            feedUser1.Posts.Add(postUser4_2);
            feedUser2.Posts.Add(postUser4_2);
            feedUser3.Posts.Add(postUser4_2);
            feedUser4.Posts.Add(postUser4_2);







            BlockedColl.InsertOne(blockedListUser1);

            FeedColl.InsertOne(feedUser1);
            FeedColl.InsertOne(feedUser2);
            FeedColl.InsertOne(feedUser3);
            FeedColl.InsertOne(feedUser4);

            PostColl.InsertOne(postUser1_1);
            PostColl.InsertOne(postUser1_2);
            PostColl.InsertOne(postUser2_1);
            PostColl.InsertOne(postUser2_2);
            PostColl.InsertOne(postUser3_1);
            PostColl.InsertOne(postUser3_2);
            PostColl.InsertOne(postUser4_1);
            PostColl.InsertOne(postUser4_2);

            CommentColl.InsertOne(commentUser1);
            CommentColl.InsertOne(commentUser2);
            CommentColl.InsertOne(commentUser3);
            CommentColl.InsertOne(commentUser4);


            UserColl.FindOneAndUpdate(a => a.Name == "Zacher", Builders<User>.Update.Set(z => z.MyWall, wallUser1));
            UserColl.FindOneAndUpdate(a => a.Name == "Zacher", Builders<User>.Update.Set(z => z.MyBlockedUsersId, blockedListUser1.Id));
            UserColl.FindOneAndUpdate(a => a.Name == "Tobi", Builders<User>.Update.Set(z => z.MyWall, wallUser2));

            UserColl.FindOneAndUpdate(a => a.Name == "Andy", Builders<User>.Update.Set(z => z.MyWall, wallUser3));

            UserColl.FindOneAndUpdate(a => a.Name == "Engholm", Builders<User>.Update.Set(z => z.MyWall, wallUser4));


        }
    }
}
