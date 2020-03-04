using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.Model
{
    public class Track
    {
        public Guid id { get; set; }
        public int shorturlid { get; set; }
        public string ipaddress { get; set; }
        public string referrerurl { get; set; }
        public DateTime createdat { get; set; }
    }
}
