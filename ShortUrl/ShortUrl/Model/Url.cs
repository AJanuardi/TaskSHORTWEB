using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.Model
{
    public class Url
    {
        public int id { get; set; }
        public string title { get; set; }
        public string shorturl { get; set; }
        public string url { get; set; }
        public int userid { get; set; }
        public DateTime createdat { get; set; }
    }
}
