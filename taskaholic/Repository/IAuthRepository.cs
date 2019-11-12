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
    }
}
