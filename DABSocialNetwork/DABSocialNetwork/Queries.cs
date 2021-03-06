﻿using System;
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
        private IMongoCollection<Circle> circleCollection;
        private IMongoCollection<Wall> wallCollection;
        private IMongoCollection<User> userCollection;
        private IMongoCollection<BlockedUsers> blockedCollection;

        public Queries(string nameOfDb)
        {
            db = client.GetDatabase(nameOfDb);
            feedCollection = db.GetCollection<Feed>("Feed");
            circleCollection = db.GetCollection<Circle>("Circle");
            wallCollection = db.GetCollection<Wall>("Wall");
            userCollection = db.GetCollection<User>("User");
            blockedCollection = db.GetCollection<BlockedUsers>("BlockedUsers");
        }
        
        public void Feed(ObjectId logged_in_users_id)
        {
            var user = userCollection.Find(u => u.Id == logged_in_users_id).ToList()[0];

            var myFeed = feedCollection.Find(a => a.User_Id == user.Id).ToList()[0];
            if (myFeed != null)
            {

                Console.WriteLine();
                Console.WriteLine("Seeing the feed for: {0} with the ID: {1}", user.Name, myFeed.User_Id);


                // Writes the content of the posts to the console
                for (int i = 0; (i < 7) && (i < myFeed.Posts.Count); i++)
                {
                    if (myFeed.Posts[i] != null)
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

                        if (myFeed.Posts[i].Comments.Count != 0)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                if (myFeed.Posts[i].Comments[j] != null)
                                {
                                    Console.WriteLine("\nThe content of the comment is: {0}",
                                        myFeed.Posts[i].Comments[j].CommentContent);
                                    //Console.WriteLine("By: {0}", myFeed.Posts[i].Comments[j].User_id);
                                    Console.WriteLine("At time and date: {0}",
                                        myFeed.Posts[i].Comments[j].TimeOfCommenting);
                                }
                            }
                        }

                        Console.WriteLine("___________________________________________________");
                    }
                }
            }
        }

        public void Wall(ObjectId nameOfUserToVisit, ObjectId nameOfVisitingUser)
        {
            var isHeBlocked = false;
            var visitingUser = userCollection.Find(a => a.Id == nameOfVisitingUser).ToList()[0];
            var userToVisit = userCollection.Find(a => a.Id == nameOfUserToVisit).ToList()[0];

            try
            {
                var blockedUsers = blockedCollection.Find(a => a.Id == userToVisit.MyBlockedUsersId).ToList()[0];

                foreach (var VARIABLE in blockedUsers.MyBlockedUsers)
                {
                    if (VARIABLE == visitingUser.Id)
                        isHeBlocked = true;
                }
            }
            catch (Exception e)
            {
                isHeBlocked = false;
            }
            
            if (visitingUser != null && userToVisit != null && !isHeBlocked)
            {
                Console.WriteLine("Viewing the wall of: {0}", userToVisit.Name);
                Console.WriteLine("___________________________________________________");

                foreach (var post in userToVisit.MyWall.UserPosts)
                {
                    if (string.IsNullOrEmpty(post.CircleName) || PartOfCircle(visitingUser, post.CircleName))
                    {
                        Console.WriteLine("Image of the post: {0}", post.Image);
                        Console.WriteLine("Text of the post: {0}", post.Text);
                        Console.WriteLine("Time of the post: {0}", post.TimeOfPosting);
                        Console.WriteLine("___________________________________________________");
                    }
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
            var listOfCircles = circleCollection.Find(a => a.NameOfCircle != null).ToList();
            foreach (var userCircle in listOfCircles)
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
