using Data_Access_layer.DTO;
using Data_Access_layer.Repository.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Buisness_Logic_Layer.Managers
{
    public class UserBLL
    {
        private readonly Data_Access_layer.Services.UserDAL _DAL;
        private readonly IConfiguration _configuration;

        public UserBLL(Data_Access_layer.Services.UserDAL DAL,
            IConfiguration configuration)
        {
            _DAL = DAL;
            _configuration = configuration;
        }
        
        //Getting all the users
        public async Task<IEnumerable<UserDto>> GetUser()
        {
            var res =  await _DAL.GetUser();
            return res.Select(x => UserDto.MapToDTO(x));
        }
        
        //Registering the user and converting password to Hash
        public async Task<bool> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            if (await _DAL.Register(user) == null)
            {
                return false;
            }
            return true;
        }

        //Loging the user and generating JWT Token
        public async Task<string> Login(UserDto request)
        {
            var user = await _DAL.Login(request);
            if (user == null)
            {
                return "Login failed";
            }
            string token = CreateToken(user);
            return token;
        }

        //Creating a Token
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        //creating a password hash
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
