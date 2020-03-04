using Microsoft.EntityFrameworkCore;
using ShortUrl.Model;

namespace ShortUrl.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public AppDbContext(DbContextOptions options) : base (options)
        {
        }
    }
}
