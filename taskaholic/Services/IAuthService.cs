using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskaholic.DTOs;
using taskaholic.Models;

namespace taskaholic.Services
{
    public interface IAuthService
    {
        Task<User> CreateUser(User user, string password);
        Task<User> GetUser(string email,string password);
        Task<List<User>> GetUsers(string searchCriteriaEmail);
        Task<bool> ChangeUserActive(string Id,bool isActive);
        bool CheckPassword(string senha, string senhaConfirmação);
        bool CheckRole(string role);
        string GenerateToken(User user);
        Task<bool> ChangeUserData(UserChangeDTO user);
    }
}
