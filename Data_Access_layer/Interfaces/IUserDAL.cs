using Data_Access_layer.DTO;
using Data_Access_layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_layer.Interfaces
{
    public interface IUserDAL
    {
         Task<IEnumerable<User>> GetUser();
         Task<bool> Register(User request);

         Task<User> Login(UserDto request);
    }
}
