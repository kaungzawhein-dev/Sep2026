
using FootballWorld.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballWorld.Data
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(
            DbContextOptions<FootballDbContext> options)
            : base(options)
        {
        }

        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
