using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.Models;

namespace taskaholic.Repository
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string email);
        Task<bool> UserExists(string username);
        Task<List<User>> FindUsers(string email);
        Task<bool> ChangeUserActive(string Id, bool isActive);
        Task<bool> Update(string Id,string Name, string Role, string Email, byte[] Password, byte[] PasswordSalt,bool isActive);
    }
}
