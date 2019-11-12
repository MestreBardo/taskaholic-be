using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.Models;

namespace taskaholic.Services
{
    public interface IAuthService
    {
        Task<User> CreateUser(User user, string password);
        Task<User> GetUser(string email,string password);
        string GenerateToken(User user);
    }
}
