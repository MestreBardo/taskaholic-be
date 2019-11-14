using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.Models;

namespace taskaholic.DTOs
{
    public class UserChangeDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
  
        public string Email { get; set; }
       
        public string Password { get; set; }
        public string ReinputPassword { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }
        public bool isActive { get; set; }
    }
}
