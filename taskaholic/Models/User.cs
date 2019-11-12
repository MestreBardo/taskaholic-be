using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskaholic.Models
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "O Nome é necessário para o cadastro")]
        [StringLength(100,MinimumLength = 3, ErrorMessage = "O nome precisa ter mais de três letras")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O Email é necessário para o cadastro")]
        [EmailAddress(ErrorMessage = "Por favor inserir um email valido.")]
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public List<Assignment> Assignments{ get; set;}
    }
}
