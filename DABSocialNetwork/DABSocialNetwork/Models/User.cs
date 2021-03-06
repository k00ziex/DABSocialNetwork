﻿using System.Collections.Generic;
using MongoDB.Bson;

namespace DABSocialNetwork.Models
{
    public class User
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        // Yes, you can be an attack helicopter if you'd like
        public string Gender { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public Wall MyWall{ get; set; }

        public List<ObjectId> MyCircles { get; set; }

        public ObjectId MyBlockedUsersId { get; set; }
    }
}