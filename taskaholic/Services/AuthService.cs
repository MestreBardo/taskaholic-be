using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using taskaholic.DataBaseModels;
using taskaholic.Models;
using taskaholic.Repository;

namespace taskaholic.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IUserDatabaseSettings settings, IConfiguration configuration)
        {
            _configuration = configuration;
            _authRepository = new AuthRepository(settings);
        }
        public async Task<User> CreateUser(User user, string password)
        {
            
            user.Name = user.Name.ToLower();
            user.Email = user.Email.ToLower();
            if (!await _authRepository.UserExists(user.Email)) 
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    user.PasswordSalt = hmac.Key;
                    user.Password = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
                await _authRepository.Register(user);
                return user;
            }
            else 
            {
                return null;
            }
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> GetUser(string email, string password)
        {
            email = email.ToLower();
            User user = await _authRepository.Login(email);
            if (user == null)
                return null;
            using (var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
            {
                byte[] passwordHash;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(var i = 0; i<passwordHash.Length; i++)
                {
                    if(passwordHash[i] != user.Password[i])
                    {
                        return null;
                    }
                }
            }
            return user;
        }
    }
}
