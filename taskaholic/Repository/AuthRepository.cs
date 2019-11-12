using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.DataBaseModels;
using taskaholic.Models;

namespace taskaholic.Repository
{
    
    public class AuthRepository : IAuthRepository
    {
        private readonly IMongoCollection<User> _user;
        public AuthRepository(IUserDatabaseSettings settings)
        {
            MongoClient _client = new MongoClient(settings.ConnectionString);
            var _database = _client.GetDatabase(settings.DatabaseName);
            _user = _database.GetCollection<User>(settings.UsersCollectionName);

        }
        public async Task<User> Login(string email)
        {
            return _user.Find(x => x.Email == email).FirstOrDefault();
        }

        public async Task<User> Register(User user)
        {
            await _user.InsertOneAsync(user);
            return user;
        }

        public Task<User> Register(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _user.Find(x => x.Email == email).FirstOrDefaultAsync() == null)
            {
                return false;
            }
            else 
            {
                return true;
            };
        }
    }
}
