using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Repositories;
using Org.BouncyCastle.Crypto.Generators;
using Data.Entities;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepo _userrepo;
        private const string Salt = "Amamro porano jaha chai, tumi tai";
        private const double SaltExpire = 7;
        private const int SaltGeneratorLogRounds = 12;
        public UserServices(IUserRepo userrepo)
        {
            _userrepo = userrepo;
        }

        private string GenerateJwtToken(string userId)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] tokenKey = Encoding.ASCII.GetBytes(Salt);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserId", userId)
                }),
                Expires = DateTime.UtcNow.AddDays(SaltExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string AuthenticateUser(string userName, string password)
        {
            try
            {
                User user = _userrepo.AsQueryable().FirstOrDefault(x => x.UserName == userName);
                if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(password, user.UserPassword))
                    return GenerateJwtToken(user.UserId);
                else
                    return "";
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool RegisterUser(UserModel userModel)
        {
            try
            {
                _userrepo.Add(new Data.Entities.User
                {
                    UserId = Guid.NewGuid().ToString(),
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    UserName = userModel.UserName,
                    UserPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(userModel.Password)
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
