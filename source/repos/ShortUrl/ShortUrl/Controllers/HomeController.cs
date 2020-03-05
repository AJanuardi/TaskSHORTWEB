using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Model;
using ShortUrl.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace ShortUrl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IConfiguration Configuration;
        private AppDbContext _appDbContext;
        public HomeController(IConfiguration configuration, AppDbContext appDbContext)
        {
            Configuration = configuration;
            _appDbContext = appDbContext;
        }

        // POST: api/Home
        [HttpPost("ShortUrl")]
        public IActionResult ShortUrl(Url create)
        {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[6];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var shorturl = new String(stringChars);

            Url add = new Url()
            {
                title = create.title,
                shorturl = shorturl,
                url = create.url,
                userid = 0,
                createdat = DateTime.Now,
                };
                _appDbContext.Urls.Add(add);
                _appDbContext.SaveChanges();
                return Ok(add);
        }

        [Authorize]
        // POST: api/Home
        [HttpPost("ShortUrlCustom")]
        public IActionResult ShortUrlCustom(Url create)
        {
            var text = System.IO.File.ReadAllLines("Token.txt").Last();
            var token = text;
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;
            var sub = securityToken.Claims.First(u => u.Type == "sub").Value;
            var last = from l in _appDbContext.Users where l.id == Convert.ToInt32(sub) select l;
            var x = last.First();
                    var shorturl = create.shorturl;

                    Url add = new Url()
                    {
                        title = create.title,
                        shorturl = shorturl,
                        url = create.url,
                        userid = Convert.ToInt32(x.id),
                        createdat = DateTime.Now,
                    };
                    _appDbContext.Urls.Add(add);
                    _appDbContext.SaveChanges();
                    return Ok(add);
        }
    }
}
