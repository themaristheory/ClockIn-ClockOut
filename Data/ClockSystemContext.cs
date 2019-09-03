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

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClockEvent> ClockEvents { get; set; }
    }
}