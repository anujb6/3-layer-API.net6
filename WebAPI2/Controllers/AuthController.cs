using Data_Access_layer.DTO;
using Data_Access_layer.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI2.Controllers
{
    public class AuthController : Controller
    {

        private Buisness_Logic_Layer.Managers.UserBLL _BLL;
        private readonly IConfiguration _configuration;

        public AuthController(Buisness_Logic_Layer.Managers.UserBLL BLL, IConfiguration configuration)
        {
            _configuration = configuration;
            _BLL = BLL;
        }
        [HttpGet("Users")]

        public async Task<IEnumerable<UserDto>> GetUser()
        {
            return await _BLL.GetUser();
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(UserDto request)
        {
            return await _BLL.Register(request);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            return await _BLL.Login(request);
        }
    }
}
