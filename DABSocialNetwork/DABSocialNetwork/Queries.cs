using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DABSocialNetwork.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DABSocialNetwork
{
    public class Queries
    {
        private MongoClient client =  new MongoClient();
        private IMongoDatabase db;
        private IMongoCollection<Feed> feedCollection;
        private IMongoCollection<Post> postCollection;
        private IMongoCollection<Wall> wallCollection;
        private IMongoCollection<User> userCollection;

        public Queries(string nameOfDb)
        {
            db = client.GetDatabase(nameOfDb);
            feedCollection = db.GetCollection<Feed>("Feed");
            postCollection = db.GetCollection<Post>("Post");
            wallCollection = db.GetCollection<Wall>("Wall");
            userCollection = db.GetCollection<User>("User");
        }
        
        public void Feed(string logged_in_users_name)
        {
            var user = userCollection.Find(u => u.Name == logged_in_users_name).ToList()[0];

            var myFeed = feedCollection.Find(a => a.User_Id == user.Id).ToList()[0];
            Console.WriteLine();
            Console.WriteLine("Seeing the feed for: {0} with the ID: {1}", user.Name, myFeed.User_Id);


            // Writes the content of the posts to the console
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("___________________________________________________");
                Console.WriteLine("User for the post: {0}", myFeed.Posts[i].UserId.ToString());
                if (myFeed.Posts[i].Image != null)
                {
                    Console.WriteLine("Image for the post: {0}", myFeed.Posts[i].Image);
                }

                if (myFeed.Posts[i].Text != null)
                {
                    Console.WriteLine("Text for the post: {0}", myFeed.Posts[i].Text);
                }

                Console.WriteLine("Time of post: {0}", myFeed.Posts[i].TimeOfPosting);

                Console.WriteLine("The post has {0} comments.", myFeed.Posts[i].Comments.Count);

                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine("\nFirst comment is: {0}", myFeed.Posts[i].Comments[j].CommentContent);
                    //Console.WriteLine("By: {0}", myFeed.Posts[i].Comments[j].User_id);
                    Console.WriteLine("At time and date: {0}", myFeed.Posts[i].Comments[j].TimeOfCommenting);
                }

                Console.WriteLine("___________________________________________________");
            }
        }

        public void Wall(string nameOfUserToVisit, string nameOfVisitingUser)
        {
            var visitingUser = userCollection.Find(a => a.Name == nameOfVisitingUser).ToList()[0];
            var userToVisit = userCollection.Find(a => a.Name == nameOfUserToVisit).ToList()[0];
            var visitingWall = wallCollection.Find(a => a.UserId == userToVisit.Id).ToList()[0];

            Console.WriteLine("Viewing the wall of: {0}", userToVisit.Name);
            Console.WriteLine("___________________________________________________");

            foreach (var post in visitingWall.UserPosts)
            {
                if (post.CircleName == null || PartOfCircle(visitingUser, post.CircleName))
                {
                    Console.WriteLine("Image of the post: {0}", post.Image);
                    Console.WriteLine("Text of the post {0}", post.Text);
                    Console.WriteLine("Time of the post: {0}", post.TimeOfPosting);
                    Console.WriteLine("___________________________________________________");
                }
            }
        }


        /// <summary>
        /// User for figuring out whether the visiting user is part of the circle of the post he's trying to see
        /// </summary>
        /// <param name="User">
        /// The user that's visiting a wall of another user
        /// </param>
        /// <param name="Circle">
        /// The circle we are comparing it to
        /// </param>
        /// <returns>
        /// Returns true if the user is part of the circle, false if the user isn't.
        /// </returns>
        public bool PartOfCircle(User User, string CircleName)
        {
            foreach (var userCircle in User.MyCircles)
            {
                if (userCircle.NameOfCircle == CircleName)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
