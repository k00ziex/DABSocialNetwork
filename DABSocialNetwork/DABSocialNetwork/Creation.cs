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


        public Creation(string nameOfDb)
        {
            db = client.GetDatabase(nameOfDb);
            postCollection = db.GetCollection<Post>("Post");
            commentCollection = db.GetCollection<Comment>("Comment");
        }

        public void CreatePost(ObjectId owner_id, string image, string text, Circle circle)
        {
            try
            {
                var post = new Post(){Comments = new List<Comment>(), Circle = circle, Image = image, Text = text, UserId = owner_id};

                post.TimeOfPosting = DateTime.Now;
                postCollection.InsertOne(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CreateComment(ObjectId post_id, Comment comment)
        {
            try
            {
                comment.Post.Id = post_id;
                commentCollection.InsertOne(comment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
