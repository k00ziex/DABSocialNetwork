using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DABSocialNetwork.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DABSocialNetwork
{
    public class Creation
    {
        private MongoClient client = new MongoClient();
        private IMongoDatabase db;
        private IMongoCollection<Post> postCollection;
        private IMongoCollection<Comment> commentCollection;
        private IMongoCollection<User> userCollection;
        private IMongoCollection<Wall> wallCollection;
        private IMongoCollection<Feed> feedCollection;
        private IMongoCollection<Circle> circleCollection;


        public Creation(string nameOfDb)
        {
            db = client.GetDatabase(nameOfDb);
            postCollection = db.GetCollection<Post>("Post");
            commentCollection = db.GetCollection<Comment>("Comment");
            userCollection = db.GetCollection<User>("User");
            wallCollection = db.GetCollection<Wall>("Wall");
            feedCollection = db.GetCollection<Feed>("Feed");
            circleCollection = db.GetCollection<Circle>("Circle");
        }

        public void CreatePost(ObjectId owner_id, string image, string text, string circleName)
        {
            try
            {
                var post = new Post(){Id = ObjectId.GenerateNewId(),Comments = new List<Comment>(), CircleName = circleName, Image = image, Text = text, UserId = owner_id, TimeOfPosting = DateTime.Now};
                var user = userCollection.Find(u => u.Id == owner_id);
                if(user != null)
                    postCollection.InsertOne(post);

                var userWall = userCollection.Find(x => x.Id == owner_id).ToList()[0];
                userWall.MyWall.UserPosts.Add(post);
                userCollection.FindOneAndReplace(x => x.Id == userWall.Id, userWall);

                if (!string.IsNullOrEmpty(circleName) && userWall.MyCircles != null)
                {
                    foreach (var circleID in userWall.MyCircles)
                    {
                        var circles = circleCollection.Find(x => x.Id == circleID).ToList()[0];
                        foreach (var userId in circles.Users)
                        {
                            var feedToUpdate = feedCollection.Find(x => x.User_Id == userId).ToList()[0];
                            feedToUpdate.Posts.Add(post);
                            feedCollection.FindOneAndReplace(x => x.Id == feedToUpdate.Id, feedToUpdate);
                        }
                    }
                }

                var users = userCollection.Find(x => x.Id != null).ToList();
                foreach (var userToCheck in users)
                {
                    if(string.IsNullOrEmpty(post.CircleName))
                    {
                        var feedsToUpdate = feedCollection.Find(x => x.User_Id == userToCheck.Id).ToList();
                        foreach (var feed in feedsToUpdate)
                        {
                            feed.Posts.Add(post);
                            feedCollection.FindOneAndReplace(x => x.Id == feed.Id, feed);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CreateComment(ObjectId post_id, string content)
        {
            try
            {
                var comment = new Comment(){CommentContent = content, PostId = post_id, TimeOfCommenting = DateTime.Now, Id = ObjectId.GenerateNewId()};
                var post = postCollection.Find(x => x.Id == post_id).ToList()[0];
                post.Comments.Add(comment);
                postCollection.FindOneAndReplace(x => x.Id == post_id, post);

                commentCollection.InsertOne(comment);

                var user = userCollection.Find(x => x.Id == post.UserId).ToList()[0];
                foreach (var postInWall in user.MyWall.UserPosts)
                {
                    if (postInWall.Id == post.Id)
                        postInWall.Comments.Add(comment);
                }

                userCollection.FindOneAndReplace(x => x.Id == user.Id, user);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
