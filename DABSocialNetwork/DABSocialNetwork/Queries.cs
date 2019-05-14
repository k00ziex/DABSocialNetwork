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
        private List<Post> feedPosts;
        private List<Post> wallPosts = new List<Post>();

        public Queries(string nameOfDb)
        {
            db = client.GetDatabase(nameOfDb);
            feedCollection = db.GetCollection<Feed>("Feed");
            postCollection = db.GetCollection<Post>("Post");
        }
        
        public List<Post> Feed(ObjectId logged_in_user_id)
        {
            var myFeed = feedCollection.Find(a => a.User_Id == logged_in_user_id).ToList()[0];
            foreach (var post in myFeed.Posts)
            {
                feedPosts = postCollection.Find(a => a.Id == post.Id).Limit(5).ToList();
            }

            return feedPosts;
        }

        public List<Post> Wall(ObjectId user_id, ObjectId guest_id)
        {
            var visitingWall = wallCollection.Find(a => a.UserId == user_id).ToList()[0];
            foreach (var post in visitingWall.UserPosts)
            {
                if (post.Circle == null)
                {
                    wallPosts.Add(post);
                }
            }
            return wallPosts;
        }


    }
}
