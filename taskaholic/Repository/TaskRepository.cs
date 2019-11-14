using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.DataBaseModels;
using taskaholic.DTOs;
using taskaholic.Models;

namespace taskaholic.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<User> _user;
        public TaskRepository(IUserDatabaseSettings settings)
        {
            MongoClient _client = new MongoClient(settings.ConnectionString);
            var _database = _client.GetDatabase(settings.DatabaseName);
            _user = _database.GetCollection<User>(settings.UsersCollectionName);

        }

        public async Task<List<Tarefa>> CreateTask(TarefaDTO tarefa)
        {
            var SearchId = new ObjectId(tarefa.Id);
            var filter = Builders<User>.Filter.Eq("Id", SearchId);
            var updateDef = Builders<User>.Update.Set("Assignments", tarefa.Tarefas);
            var result = await _user.FindOneAndUpdateAsync(filter, updateDef);
            return result.Assignments;
        }
        public async Task<List<Tarefa>> GetTarefas(TarefaDTO tarefa)
        {
            var SearchId = new ObjectId(tarefa.Id);
            var filter = Builders<User>.Filter.Eq("Id", SearchId);
            var result = await _user.Find(filter).FirstOrDefaultAsync();
            return result.Assignments;
        }
    }
}
