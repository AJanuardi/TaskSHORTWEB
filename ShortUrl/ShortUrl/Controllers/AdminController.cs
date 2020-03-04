using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShortUrl.Data;
using ShortUrl.Model;

namespace ShortUrl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IConfiguration Configuration;
        private AppDbContext _appDbContext;
        public AdminController(IConfiguration configuration, AppDbContext appDbContext)
        {
            Configuration = configuration;
            _appDbContext = appDbContext;
        }

        [Authorize]
        // POST: api/Admin
        [HttpPost("DataDay")]
        public IActionResult DataDay(Track date)
        {
            var x = from i in _appDbContext.Tracks where i.createdat.Day == date.createdat.Day select i;
            var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
            Dictionary<int, int> List = new Dictionary<int, int>();
            foreach(var a in q)
            {
                List.Add(a.Number, a.Count);
            }
            return Ok(List);
        }

        [Authorize]
        [HttpPost("DataMonth")]
        public IActionResult DataMonth(Track date)
        {
            var x = from i in _appDbContext.Tracks where i.createdat.Month == date.createdat.Month select i;
            var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
            Dictionary<int, int> List = new Dictionary<int, int>();
            foreach (var a in q)
            {
                List.Add(a.Number, a.Count);
            }
            return Ok(List);
        }

        [Authorize]
        [HttpPost("DataYear")]
        public IActionResult DataYear(Track date)
        {
            var x = from i in _appDbContext.Tracks where i.createdat.Year == date.createdat.Year select i;
            var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
            Dictionary<int, int> List = new Dictionary<int, int>();
            foreach (var a in q)
            {
                List.Add(a.Number, a.Count);
            }
            return Ok(List);
        }
    }
}
