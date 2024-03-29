﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.Models;

namespace taskaholic.DTOs
{
    public class UserRegisterDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="O email digitado é invalido")]
        public string Email { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "A senha precisa ter no minimo 8 caracteres")]
        public string Password { get; set; }
        public string ReinputPassword { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome precisa ter mais de três letras")]
        public string Name { get; set; }
        [Required(ErrorMessage ="É necessário possuir uma permissão")]
        public string Role { get; set; }
        public bool isActive { get; set; }
    }
}
