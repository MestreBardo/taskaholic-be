using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.DataBaseModels;
using taskaholic.Models;

namespace taskaholic.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;
        public UserService(IUserDatabaseSettings settings)
        {
            MongoClient _client = new MongoClient(settings.ConnectionString);
            var _database = _client.GetDatabase(settings.DatabaseName);
            _user = _database.GetCollection<User>(settings.UsersCollectionName);

        }
        public async Task<List<User>> Get() => 
            await _user.Find(user => true).ToListAsync();

        public User Get(string name) => _user.Find(x => x.Name == name).FirstOrDefault();
        public async Task Create(User usuario) => await _user.InsertOneAsync(usuario);

        public async Task Delete(User usuario) 
        { 
            await _user.FindOneAndDeleteAsync(x => x.Name == usuario.Name); 
        }

    }
}
