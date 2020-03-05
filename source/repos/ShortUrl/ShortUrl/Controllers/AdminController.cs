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

        [HttpGet]
        public IActionResult Get()
        {
            var x = from i in _appDbContext.Urls select i;
            return Ok(x);
        }

        [Authorize]
        // POST: api/Admin
        [HttpPost("Data")]
        public IActionResult Data(Date date)
        {
            if (Convert.ToInt32(date.shortid) == 0 && Convert.ToInt32(date.day) == 0 && Convert.ToInt32(date.month) == 0)
            {
                var x = from i in _appDbContext.Tracks where i.createdat.Year == date.year select i;
                var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
                Dictionary<int, int> List = new Dictionary<int, int>();
                foreach (var a in q)
                {
                    List.Add(a.Number, a.Count);
                }
                return Ok(List);
            }

            if (Convert.ToInt32(date.shortid) == 0 && Convert.ToInt32(date.year) != 0 && Convert.ToInt32(date.month) != 0)
            {
                var x = from i in _appDbContext.Tracks where (i.createdat.Year == date.year && i.createdat.Month == date.month) select i;
                var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
                Dictionary<int, int> List = new Dictionary<int, int>();
                foreach (var a in q)
                {
                    List.Add(a.Number, a.Count);
                }
                return Ok(List);
            }

            if (Convert.ToInt32(date.shortid) == 0 && Convert.ToInt32(date.year) != 0 && Convert.ToInt32(date.month) != 0 && Convert.ToInt32(date.day) != 0)
            {
                var x = from i in _appDbContext.Tracks where (i.createdat.Year == date.year && i.createdat.Month == date.month && i.createdat.Day == date.day) select i;
                var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
                Dictionary<int, int> List = new Dictionary<int, int>();
                foreach (var a in q)
                {
                    List.Add(a.Number, a.Count);
                }
                return Ok(List);
            }

            if (Convert.ToInt32(date.shortid) != 0 && Convert.ToInt32(date.day) == 0 && Convert.ToInt32(date.month) == 0)
            {
                var x = from i in _appDbContext.Tracks where (i.createdat.Year == date.year && i.shorturlid == date.shortid) select i;
                var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
                Dictionary<int, int> List = new Dictionary<int, int>();
                foreach (var a in q)
                {
                    List.Add(a.Number, a.Count);
                }
                return Ok(List);
            }

            if (Convert.ToInt32(date.shortid) != 0 && Convert.ToInt32(date.year) != 0 && Convert.ToInt32(date.month) != 0)
            {
                var x = from i in _appDbContext.Tracks where (i.createdat.Year == date.year && i.createdat.Month == date.month && i.shorturlid == date.shortid) select i;
                var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
                Dictionary<int, int> List = new Dictionary<int, int>();
                foreach (var a in q)
                {
                    List.Add(a.Number, a.Count);
                }
                return Ok(List);
            }

            if (Convert.ToInt32(date.shortid) != 0 && Convert.ToInt32(date.year) != 0 && Convert.ToInt32(date.month) != 0 && Convert.ToInt32(date.day) != 0)
            {
                var x = from i in _appDbContext.Tracks where (i.createdat.Year == date.year && i.createdat.Month == date.month && i.createdat.Day == date.day && i.shorturlid == date.shortid) select i;
                var q = x.GroupBy(x => x.shorturlid).Select(g => new { Number = g.Key, Count = g.Count() });
                Dictionary<int, int> List = new Dictionary<int, int>();
                foreach (var a in q)
                {
                    List.Add(a.Number, a.Count);
                }
                return Ok(List);
            }

            return Ok("Fail Get Data");
        }

        [Authorize]
        // POST: api/Admin
        [HttpPost("Link")]
        public IActionResult Link(Url url)
        {
            var x = from i in _appDbContext.Urls select i;
            var q = x.GroupBy(x => x.shorturl).Select(g => new { Number = g.Key, Count = g.Count() });
            Dictionary<string, int> List = new Dictionary<string, int>();
            foreach (var a in q)
            {
                List.Add(a.Number, a.Count);
            }
            return Ok(List);

        }

        [Authorize]
        // POST: api/Admin
        [HttpPost("Short")]
        public IActionResult Short(IP iP)
        {
            Console.WriteLine(iP.sht);
            var x = from i in _appDbContext.Tracks where i.shorturlid == iP.sht select i;
            var q = x.GroupBy(x => x.ipaddress).Select(g => new { Number = g.Key, Count = g.Count() });
            Dictionary<string, int> List = new Dictionary<string, int>();
            foreach (var a in q)
            {
                List.Add(a.Number, a.Count);
            }
            return Ok(List);

        }

        public class Date
        {
            public int shortid { get; set; }
            public int year { get; set; }
            public int month { get; set; }
            public int day { get; set; }
        }

        public class IP
        {
            public int sht {get; set;}
        }
    }
}
