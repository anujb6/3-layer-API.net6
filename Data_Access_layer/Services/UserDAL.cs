using Data_Access_layer.DTO;
using Data_Access_layer.Interfaces;
using Data_Access_layer.Repository;
using Data_Access_layer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Data_Access_layer.Repository.Models.User;

namespace Data_Access_layer.Services
{
    public class UserDAL 
    {
        private readonly IssueDbContext _context;
        private readonly IConfiguration _configuration;

        public UserDAL(IssueDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        //Getting all the Users
        public async Task<IEnumerable<User>> GetUser()
        {
            var res  = await _context.Users.ToListAsync();
            return res;
        }
        
        //Registering the user
        public async Task<bool> Register(User request)
        {
            await _context.AddAsync<User>(request);
            var inserted = await _context.SaveChangesAsync();
            return inserted == 1 ? true : false;
        }

        //Login authentication
        public async Task<User> Login(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.Username == request.Username
            );

            var pass = Encoding.Default.GetString(VerifyPasswordHash(request.Password, user.PasswordSalt));
            var userpass = Encoding.Default.GetString (user.PasswordHash);

            if (pass.Equals(userpass)) {
                return user;
            }
            return null;
        }
        public byte[] VerifyPasswordHash(string password, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash;
            }
        }
    }
}
