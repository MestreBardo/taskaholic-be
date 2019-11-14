using MongoDB.Bson;
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

        public async Task<bool> ChangeUserActive(string Id, bool isActive)
        {
            var SearchId = new ObjectId(Id);
            var filter = Builders<User>.
            Filter.Eq("Id", SearchId);
                var updateDef = Builders<User>.Update.
            Set("IsActive", isActive);
            var result = await _user.UpdateOneAsync(filter, updateDef);
            if (result.IsAcknowledged)
            {
                return true;
            }
            return false;
             
        }

        public async Task<List<User>> FindUsers(string email)
        {
            List<User> users =  await _user.Find(x => x.Email == email).Project<User>(Builders<User>.Projection.Exclude(x => x.Password).Exclude(x => x.PasswordSalt).Exclude(x => x.Assignments)).ToListAsync();
            return users;
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

        public async Task<bool> Update(string Id, string Name, string Role, string Email, byte[] Password, byte[] PasswordSalt,bool isActive)
        {
            var SearchId = new ObjectId(Id);
            var filter = Builders<User>.Filter.Eq("Id", SearchId);
            var updateDef = Builders<User>.Update.Set("IsActive",isActive);
            if (Name != null)
            {
                updateDef = updateDef.Set("Name", Name);
            }
            if (Role != null)
            {
                updateDef = updateDef.Set("Role", Role);
            }
            if (Email != null)
            {
                updateDef = updateDef.Set("Email", Email);
            }
            if (Password != null)
            {
                updateDef = updateDef.Set("Password", Password);
                updateDef = updateDef.Set("PasswordSalt", PasswordSalt);
            }


            var result = await _user.UpdateOneAsync(filter, updateDef);
            if (result.IsAcknowledged)
            {
                return true;
            }
            return false;
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
