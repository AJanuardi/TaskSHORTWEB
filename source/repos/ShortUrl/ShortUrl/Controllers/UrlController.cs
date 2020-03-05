using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShortUrl.Data;
using ShortUrl.Model;

namespace ShortUrl.Controllers
{
    [Route("")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private IConfiguration Configuration;
        private AppDbContext _appDbContext;
        public UrlController(IConfiguration configuration, AppDbContext appDbContext)
        {
            Configuration = configuration;
            _appDbContext = appDbContext;
        }

        // GET: api/Home/5
        [HttpGet("{url}")]
        public IActionResult Get(string url)
        {
            var IP = Request.HttpContext.Connection.RemoteIpAddress;
            var x = (from i in _appDbContext.Urls where i.shorturl == url select i);
            var y = x.First();
            if (y != null)
            {
                Track reg = new Track()
                {
                    shorturlid = y.id,
                    ipaddress = IP.ToString(),
                    referrerurl = y.url,
                    createdat = DateTime.Now,
                };
                _appDbContext.Tracks.Add(reg);
                _appDbContext.SaveChanges();
                return Redirect(y.url);
            }
            return Ok("Link Hasnt Been Activate");
        }
    }
}
