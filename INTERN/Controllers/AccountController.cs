using INTERN.DTO;
using INTERN.Model;
using INTERN.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace INTERN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(ProductContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var u = await _context.Users.FirstOrDefaultAsync(p => p.UserName == user.UserName);
            if (u == null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(new { message = "User registered successfully" });
            }
            else return Ok(new { message = "UserName is Exist" });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, dbUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
           return await _context.Users.ToListAsync();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var u = await _context.Users.FirstOrDefaultAsync(p => p.UserName == user.UserName);
            if (u != null)
            {
                User userr = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
                };
                _context.Entry(userr).State = EntityState.Modified;
                return Ok(new {Message = "Change successfully"});
            }   
            else return Ok(new { Message = "Can't find this User" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u != null)
            {
                _context.Users.Remove(u);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Delete successfully" });
            }
            else return Ok(new { Message = "Can't find this User" });
        }

    }

}
