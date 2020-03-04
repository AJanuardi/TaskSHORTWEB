using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShortUrl.Data;
using ShortUrl.Model;

namespace ShortUrl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration Configuration;
        private AppDbContext _appDbContext;
        public UserController (IConfiguration configuration, AppDbContext appDbContext)
        {
            Configuration = configuration;
            _appDbContext = appDbContext;
        }

        [Authorize]
        // GET: api/Home
        [HttpGet("Welcome")]
        public IActionResult Welcome()
        {
            var text = System.IO.File.ReadAllLines("Token.txt").Last();
            Console.WriteLine(text);
            var token = text;
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;
            Console.WriteLine(securityToken);
            if (securityToken == null)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                message = "Welcome",
            });
        }

        // POST: api/Home
        [HttpPost("Login")]
        public IActionResult Login(User login)
        {
            IActionResult response = Unauthorized();
            var data = AuthenticatedUser(login);
                var token = GenerateJwtToken(data);
                TextWriter tkn = new StreamWriter("Token.txt", true);
                tkn.WriteLine(token);
                tkn.Close();
                Console.WriteLine(token);
            Token tk = new Token()
            {
                jwt = token
            };
                return Ok(tk);
        }

        // POST: api/Home
        [HttpPost("Register")]
        public IActionResult Register(User register)
        {
            var regis = new User()
            {
                name = register.name,
                email = register.email,
                password = register.password,
            };
                _appDbContext.Users.Add(regis);
                _appDbContext.SaveChanges();
                return Ok("Account Has Been Signed");
        }

        private User AuthenticatedUser(User input)
        {
            User user = null;
            var data = from x in _appDbContext.Users select new { x.id, x.name, x.password};
            foreach (var x in data)
            {
                if (input.name == x.name && input.password == x.password)
                {
                    user = new User
                    {
                        id = x.id,
                        name = input.name,
                        password = input.password,
                    };
                    return user;
                }
            }

            return user;
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }

        public class Token
        {
            public string jwt { get; set; }
        }
    }
}
