using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.Models;

namespace taskaholic.DTOs
{
    public class TarefaDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<Tarefa> Tarefas { get; set; }
    }
}
