using DoctorPatientsManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorPatientsManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DoctorsDbContext mdbc;
        private readonly IConfiguration configuration;

        public AuthController(DoctorsDbContext m, IConfiguration ic)
        {
            this.mdbc = m;
            this.configuration = ic;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await mdbc.registerUsers.ToListAsync();
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<ActionResult> AddUser(RegisterUser r)
        {
            var users = mdbc.registerUsers.FirstOrDefault(x => x.UserName == r.UserName);
            if (users == null)
            {
                string hashpassword = BCrypt.Net.BCrypt.HashPassword(r.Password);
                string hashCpassword = BCrypt.Net.BCrypt.HashPassword(r.ConfirmPassword);
                var user = new RegisterUser() {
                UserName = r.UserName,
                Password = hashpassword,
                ConfirmPassword = hashCpassword,
                Email = r.Email,
                PhoneNo = r.PhoneNo,
                Gender = r.Gender,
                Role = r.Role,
            };



                mdbc.registerUsers.Add(user);
                mdbc.SaveChanges();
                return Ok(user);
            }
            return BadRequest(new { error = "UserName Already Exists" });

        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginUser l)
        {
            var user = await mdbc.registerUsers.FirstOrDefaultAsync(x => x.UserName == l.UserName);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(l.Password, user.Password))
                {
                    var token = CreatToken(user);
                    return Ok(new { token, user });
                }
            }
            return NotFound("User Not Found With Provided Detail");

        }
        private string CreatToken(RegisterUser ud)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ud.UserName),
                new Claim(ClaimTypes.Role,ud.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSetting:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
