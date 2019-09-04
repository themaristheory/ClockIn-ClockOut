using ClockIn_ClockOut.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClockIn_ClockOut.Data
{
    public class ClockSystemContext : DbContext
    {
        public ClockSystemContext(DbContextOptions<ClockSystemContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClockEvent> ClockEvents { get; set; }
    }
}