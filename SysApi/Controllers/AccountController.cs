using Cls.Api.Controllers;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using Models;
using SysApi.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;

namespace SysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiBaseController
    {

        private readonly IConfiguration _config;

        public AccountController(IUnitOfWork unitOfWork, IConfiguration config) : base(unitOfWork)
        {
            _config = config;
        }


        [HttpPost("RegistrationAsync")]
        public IActionResult RegistrationAsync(Registerdto user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user1 = _unitOfWork.Users
                 .Find(u => u.Username == user.Username);

            if (user1 != null)
            {
                return BadRequest("Username is already taken.");
            }


            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);



            User _user = new User()
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Username = user.Username,
                Password = user.Password,
                UserType = user.UserType,

            };


            _unitOfWork.Users.Add(_user);
            _unitOfWork.Save();

            return Ok("Done");

        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto dto)
        {   var user = _unitOfWork.Users.GetAll().FirstOrDefault(x => x.Username == dto.username && BCrypt.Net.BCrypt.Verify(dto.password, x.Password));
            if(user != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Name", user.Name.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn
                    );

                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tokenValue, User = user});
            }
            
            return Unauthorized();
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            return Ok();
        }


        [HttpGet("GetUser/{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok();
        }
    }
}
