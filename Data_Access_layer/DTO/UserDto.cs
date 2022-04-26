using Data_Access_layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_layer.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public static UserDto MapToDTO(User user)
        {
            var result = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Password = "u ain't getting it"
            };


            return result;

        }
    }
}
